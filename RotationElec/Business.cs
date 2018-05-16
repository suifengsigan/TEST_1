using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class RotationElecUI : SnapEx.BaseUI
{
    string EACT_POSITIONING_DATE = "EACT_POSITIONING_DATE";
    public override void Init()
    {
        var snapSelectElec = Snap.UI.Block.SelectObject.GetBlock(theDialog, selection0.Name);
        snapSelectElec.LabelString = "选择电极";
        snapSelectElec.MaximumScope = Snap.UI.Block.SelectionScope.AnyInAssembly;
        snapSelectElec.SetFilter(Snap.NX.ObjectTypes.Type.Body,Snap.NX.ObjectTypes.SubType.BodySolid);
        snapSelectElec.AllowMultiple = false;

        var snapSelectJiaju = Snap.UI.Block.SelectObject.GetBlock(theDialog, selectionJiaju.Name);
        snapSelectJiaju.LabelString = "选择夹具";
        snapSelectJiaju.MaximumScope = Snap.UI.Block.SelectionScope.WorkPart;
        snapSelectJiaju.SetFilter(Snap.NX.ObjectTypes.Type.Body,Snap.NX.ObjectTypes.SubType.BodySolid);
        snapSelectJiaju.AllowMultiple = false;

        tempObjs.Clear();

        RefreshUI();
    }

    public override void DialogShown()
    {
        RefreshUI();
    }

    List<DisplayableObject> tempObjs = new List<DisplayableObject>();
    public override void Apply()
    {
        Perform();
        tempObjs.Clear();
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block.Name == selection0.Name)
        {
            Snap.NX.Body body = selection0.SelectedObjects.FirstOrDefault() as Snap.NX.Body;
            if (!SnapEx.Helper.IsEactElecBody(body) || body.GetAttributeInfo().Where(u => u.Title == EACT_POSITIONING_DATE).Count() > 0)
            {
                selection0.SelectedObjects = new List<Snap.NX.NXObject>().ToArray();
            }
        }

        RefreshUI();

        Perform(true);
    }

    void Perform(bool isHighlighted = false)
    {
        var workPart = theSession.Parts.Work;
        var body = selection0.SelectedObjects.FirstOrDefault() as Snap.NX.Body;
        var jiajuBody = selectionJiaju.SelectedObjects.FirstOrDefault() as Snap.NX.Body;
        tempObjs.ForEach(u => {
            Snap.NX.NXObject snapU = u;
            snapU.Delete();
        });
        tempObjs.Clear();

        if (body != null)
        {
            Snap.NX.Body snapBody=body;
            //电极原点
            var tempElecOrigin=SnapEx.Helper.GetElecMidPosition(workPart, body);
            if (tempElecOrigin == null)
            {
                theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, "该电极未发现基准点！");
                return;
            }
            var elecOrgin =(Snap.Position)tempElecOrigin ;
            

            if (isHighlighted) 
            {
                SnapEx.Create.DisplayModification(tempObjs);
            }

            bool isPatter = false;
            if (enum01.SelectedItem == "平移")
            {
                isPatter = toggleMovePatter.Value;
            }
            else
            {
                isPatter = toggleRotationPatter.Value;
            }
            int sum = 0;

            if (isPatter) 
            {
                sum=(int)(expressionPatterSum.Value<0?0:expressionPatterSum.Value);
            }

            double distance = expressionPatterDistance.Value;
            var vector = Snap.Vector.Unit(vectorPatter.Direction);

            if (body.NXOpenBody.OwningComponent != null && snapBody.Prototype != null)
            {
                var snapComponent = snapBody.OwningComponent; ;
                NXOpen.Assemblies.Component component = snapComponent;
               

                var transForm = GetTransform(elecOrgin);

                for (int i = 0; i < sum + 1; i++) 
                {
                    Snap.NX.Component newComponent = workPart.ComponentAssembly.CopyComponents(new List<NXOpen.Assemblies.Component> { component }.ToArray()).First();
                    newComponent.Prototype.Bodies.First().SetDateTimeAttribute(EACT_POSITIONING_DATE, DateTime.Now);
                    tempObjs.Add(newComponent);

                     //平移旋转
                     var trans = transForm.Matrix;
                     Matrix3x3 matrix = new Matrix3x3();
                     matrix.Xx = trans[0]; matrix.Xy = trans[4]; matrix.Xz = trans[8];
                     matrix.Yx = trans[1]; matrix.Yy = trans[5]; matrix.Yz = trans[9];
                     matrix.Zx = trans[2]; matrix.Zy = trans[6]; matrix.Zz = trans[10];
                     workPart.ComponentAssembly.MoveComponent(newComponent, new Vector3d(trans[3], trans[7], trans[11]), matrix);

                     if (i != 0)
                     {
                         workPart.ComponentAssembly.MoveComponent(newComponent, (distance*i) * vector, new Snap.Orientation());
                     }
                }
            }
            else
            {
                NXOpen.Point point = null;
                if (!isHighlighted)
                {
                    var tempPoint = SnapEx.Helper.GetElecMidPointInPart(Snap.Globals.WorkPart, snapBody);
                    if (tempPoint != null)
                    {
                        point = tempPoint;
                    }
                }
                for (int i = 0; i < sum + 1; i++) 
                {
                    var newBody = snapBody.Copy();
                    var guid=Guid.NewGuid().ToString();
                    newBody.SetStringAttribute(SnapEx.EactConstString.EACT_ELECT_GROUP,guid );
                    newBody.SetDateTimeAttribute(EACT_POSITIONING_DATE, DateTime.Now);
                    tempObjs.Add(newBody);
                    var transForm = GetTransform(elecOrgin);
                    if (i != 0)
                    {
                        transForm = Snap.Geom.Transform.Composition(transForm, Snap.Geom.Transform.CreateTranslation((distance * i) * vector));
                    }

                    newBody.Move(transForm);
                    if (point != null) 
                    {
                        Snap.NX.Point newPoint = Snap.NX.Point.Wrap(point.Tag).Copy();
                        newPoint.SetStringAttribute(SnapEx.EactConstString.EACT_ELECT_GROUP, guid);
                        newPoint.Move(transForm);
                    }
                }
               
                
            }

            if (toggleJiaju.Value && jiajuBody != null)
            {
                for (int i = 0; i < sum + 1; i++)
                {
                    Snap.NX.Body snapJiajuBody = jiajuBody;
                    var newJiajuBody = snapJiajuBody.Copy();
                    var guid = Guid.NewGuid().ToString();
                    newJiajuBody.Layer = body.Layer;
                    newJiajuBody.SetDateTimeAttribute(EACT_POSITIONING_DATE, DateTime.Now);
                    tempObjs.Add(newJiajuBody);
                    elecOrgin = new Snap.Position((snapBody.Box.MaxX + snapBody.Box.MinX) / 2, (snapBody.Box.MaxY + snapBody.Box.MinY) / 2, snapBody.Box.MinZ);
                    var transForm = GetTransform(elecOrgin);
                    if (i != 0)
                    {
                        transForm = Snap.Geom.Transform.Composition(transForm, Snap.Geom.Transform.CreateTranslation((distance * i) * vector));
                    }

                    newJiajuBody.Move(transForm);

                }
            }
           

            if (isHighlighted) 
            {
                SnapEx.Create.DisplayModification(tempObjs);
            }
        }
    }

    Snap.Geom.Transform GetTransform(Snap.Position elecOrgin)
    {
        var transForm = Snap.Geom.Transform.CreateTranslation();

        bool isRotation = true;
        bool isMove = true;

        if (enum01.SelectedItem == "平移")
        {
            isRotation = toggleMoveRotation.Value;
        }
        else
        {
            isMove = toggleRotationMove.Value;
        }

        if (isRotation)
        {
            NXOpen.Point3d point = Snap.Globals.Wcs.Origin;
            if (enumRotation.SelectedItem == "指定点")
            {
                point = pointAxis.Position;
            }
            else if (enumRotation.SelectedItem == "电极原点")
            {
                point = elecOrgin;
            }
            transForm = Snap.Geom.Transform.Composition(transForm, Snap.Geom.Transform.CreateRotation(point, vector0.Direction, expressionAngle.Value));
        }

        if (isMove)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            if (enum0.SelectedItem == "点对点")
            {
                Snap.Position startPoint = pointStart.Position;
                Snap.Position endPoint = pointEnd.Position;
                var vector = endPoint - startPoint;
                x = vector.X;
                y = vector.Y;
                z = vector.Z;
            }
            else if (enum0.SelectedItem == "轴")
            {
                var distance = expressionDistance.Value;


                switch (enumSelectAxis.SelectedItem)
                {
                    case "X":
                        {
                            x = distance;
                            break;
                        }
                    case "Y":
                        {
                            y = distance;
                            break;
                        }
                    default:  //Z
                        {
                            z = distance;
                            break;
                        }
                }
            }
            else //增量XYZ
            {
                x = expressionDistanceX.Value;
                y = expressionDistanceY.Value;
                z = expressionDistanceZ.Value;
            }
            transForm = Snap.Geom.Transform.Composition(transForm, Snap.Geom.Transform.CreateTranslation(new Snap.Vector(x, y, z)));
        }

        return transForm;
    }

    void RefreshUI()
    {
        bool pointToPoint = enum0.SelectedItem == "点对点";
        bool axis = enum0.SelectedItem == "轴";
        bool xyz = enum0.SelectedItem == "增量XYZ";

        bool showGroupRotation = enum01.SelectedItem == "旋转";
        bool showGroupMove = enum01.SelectedItem == "平移";

        selectionJiaju.Show = toggleJiaju.Value;

        if (showGroupMove)
        {
            groupMove.Show = true;
            groupRotation.Show = toggleMoveRotation.Value;
            group.Show = toggleMovePatter.Value;
        }
        else
        {
            groupRotation.Show = !showGroupMove;
            groupMove.Show =toggleRotationMove.Value;
            group.Show = toggleRotationPatter.Value;
        }

        toggleMoveRotation.Show = false;
        toggleMoveRotation.Value = false;
        toggleMovePatter.Show = showGroupMove;
        toggleRotationMove.Show = !showGroupMove;
        toggleRotationPatter.Show = !showGroupMove;

        pointAxis.Show = enumRotation.SelectedItem == "指定点";

        pointStart.Show = pointToPoint;
        pointEnd.Show = pointToPoint;

        enumSelectAxis.Show = axis;
        expressionDistance.Show = axis;

        expressionDistanceX.Show = xyz;
        expressionDistanceY.Show = xyz;
        expressionDistanceZ.Show = xyz;
        
    }
}