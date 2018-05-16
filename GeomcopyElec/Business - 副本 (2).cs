using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

partial class GeomcopyElecUI : SnapEx.BaseUI
{
    Snap.UI.Block.SelectObject snapSelectElec = null;
    public override void Init()
    {
        snapSelectElec = Snap.UI.Block.SelectObject.GetBlock(theDialog, bodySelect0.Name);
        snapSelectElec.MaximumScope = Snap.UI.Block.SelectionScope.AnyInAssembly;
        snapSelectElec.SetFilter(Snap.NX.ObjectTypes.Type.Body, Snap.NX.ObjectTypes.SubType.BodySolid);
        snapSelectElec.AllowMultiple = true;

        var snapSelectJiaju = Snap.UI.Block.SelectObject.GetBlock(theDialog, selectionJiaju.Name);
        snapSelectJiaju.MaximumScope = Snap.UI.Block.SelectionScope.WorkPart;
        snapSelectJiaju.SetFilter(Snap.NX.ObjectTypes.Type.Body, Snap.NX.ObjectTypes.SubType.BodySolid);
        snapSelectJiaju.AllowMultiple = false;

        tempObjs.Clear();
        RefreshUI();
    }

    public override void DialogShown()
    {
        expressionX.Value = 0;
        expressionY.Value = 0;
        expressionZ.Value = 0;
        toggleJiaju.Value = false;
    }
    public override void Apply()
    {
        Perform();
        tempObjs.Clear();
    }


    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == bodySelect0)
        {
            var selectedObjs = bodySelect0.GetSelectedObjects().ToList();
            var bodies = new List<NXOpen.NXObject>();

            selectedObjs.ForEach(u => {
                var body = u as NXOpen.Body;
                if (SnapEx.Helper.IsEactElecBody(body)) 
                {
                    bodies.Add(body);
                }
            });

            if (selectedObjs.Count != bodies.Count)
            {
                bodySelect0.SetSelectedObjects(bodies.ToArray());
            }

            stringElecName.Show = bodies.Count <= 1;
           

            if (bodies.Count == 1) 
            {
                SetElecName();
            }
        }
        else if (block == stringElecName) 
        {
            var body = bodySelect0.GetSelectedObjects().FirstOrDefault() as NXOpen.Body;
            if (body != null)
            {
                if (GetElecNames().Contains(stringElecName.Value))
                {
                    SetElecName();
                }
            }
        }

       
        RefreshUI();
        Perform(true);
    }

    List<NXOpen.DisplayableObject> tempObjs = new List<NXOpen.DisplayableObject>();
    void Perform(bool isPreview=false) 
    {
        tempObjs.ForEach(u => {
            Snap.NX.NXObject snapU = u;
            snapU.Delete();
        });
        tempObjs.Clear();
        var bodies = bodySelect0.GetSelectedObjects().ToList();
        var plane = plane0.GetSelectedObjects().FirstOrDefault() as NXOpen.Plane;
        if (bodies.Count > 0 && plane != null)
        {
            int order = GetMaxOrder();
            if (bodies.Count == 1) 
            {
                int tempInt;
                if (int.TryParse(stringElecName.Value.Split('-').LastOrDefault(), out tempInt)) 
                {
                    order = tempInt - 1;
                }
            }
            bodies.ForEach(u =>
            {
                order++;
                tempObjs.Add(GeomcopyElec(isPreview,u as NXOpen.Body, plane, order));
            });
        }

        if (isPreview) 
        {
            SnapEx.Create.DisplayModification(tempObjs);
        }
    }

    NXOpen.DisplayableObject GeomcopyElec(bool isPreview,NXOpen.Body body, NXOpen.Plane plane, int order)
    {
        Snap.NX.Body SnapBody = body;
        var oldWorkPart = theSession.Parts.Work;
        var objs = new List<NXOpen.NXObject> { body };
        Snap.NX.Body newBody = null;

        if (body.OwningComponent != null && body.Prototype != null)
        {
            //析出
            SnapEx.Create.ExtractObject(objs, oldWorkPart.FullPath, false, false);
            newBody = oldWorkPart.Bodies.ToArray().FirstOrDefault(u => u.Name == body.Name);
           
        }
        else 
        {
            newBody = Snap.NX.Body.Wrap(oldWorkPart.Bodies.ToArray().FirstOrDefault(u => u.Name == body.Name).Tag).Copy();
        }

        if (newBody == null) return newBody;

        var guid = Guid.NewGuid().ToString();

        //TODO 修改名称 修改属性
        newBody.Name =GetElecName(order-1);
        newBody.SetStringAttribute("EACT_ELEC_NAME",newBody.Name);
        newBody.SetStringAttribute("EACT_ELECT_GROUP", guid);
        newBody.Move(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
        newBody.Layer = order;
        Snap.Globals.LayerStates[order] = isPreview ? Snap.Globals.LayerState.Visible : Snap.Globals.LayerState.Selectable;

        if (toggle0.Value) //继承
        {
            var markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "reflectionBody");
            var topFace = SnapEx.Helper.GetTopFace(newBody);
            var minU = topFace.BoxUV.MinU;
            var minV = topFace.BoxUV.MinV;
            var maxU = topFace.BoxUV.MaxU;
            var maxV = topFace.BoxUV.MaxV;
            var centerPosition = topFace.Position((minU + maxU) / 2, (minV + maxV) / 2);
            var normal = topFace.Normal(minU, minV);
            var point = topFace.Position(minU, minV);

            var datumPlane = Snap.Create.DatumPlane(point, normal);
            var splitBody = Snap.Create.SplitBody(newBody, datumPlane);
            var topBody = splitBody.Bodies.FirstOrDefault(b => SnapEx.Helper.GetTopFace(b) == null);

            var reflectionBody = splitBody.Bodies.FirstOrDefault(b => SnapEx.Helper.GetTopFace(b) != null);
            var newDatumPlane = Snap.Create.DatumPlane(centerPosition, new Snap.Orientation(normal).AxisY);
            reflectionBody.Move(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(centerPosition, newDatumPlane.Normal)));
            var result = Snap.Create.Unite(reflectionBody, topBody);
            result.Orphan();

            datumPlane.Delete();
            newDatumPlane.Delete();

            theSession.UpdateManager.DoUpdate(markId1);
            theSession.DeleteUndoMark(markId1, null);

            newBody = reflectionBody;
        }

        var x = expressionX.Value;
        var y = expressionY.Value;
        var z = expressionZ.Value;
        var trans = Snap.Geom.Transform.CreateTranslation(new Snap.Vector(x, y, z));
        newBody.Move(trans);
        if (!isPreview)
        {
            newBody.SetStringAttribute("EACT_MIRROR_ELECT", "Y");
            var topFace = SnapEx.Helper.GetTopFace(newBody);
            if (topFace != null) 
            {
                SetJZJAttr(topFace, newBody);
            }
            var newPoint = SnapEx.Helper.GetElecMidPoint(Snap.Globals.WorkPart, body);
            if (newPoint != null)
            {
                newPoint.SetStringAttribute(SnapEx.EactConstString.EACT_ELECT_GROUP, guid);
                newPoint.Move(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
                newPoint.Move(trans);
            }

            if (toggleJiaju.Value && selectionJiaju.Show) 
            {
                Snap.NX.Body jiaju = selectionJiaju.GetSelectedObjects().FirstOrDefault() as NXOpen.Body;
                if (jiaju != null) 
                {
                    jiaju = jiaju.Copy();
                    jiaju.Layer = order;
                    jiaju.Move(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
                    jiaju.SetStringAttribute("EACT_MIRROR_ELECT", "Y");
                    jiaju.Move(trans);
                }
            }
        }

        return newBody;
    }

    /// <summary>
    /// 设置基准角相关属性
    /// </summary>
    void SetJZJAttr(Snap.NX.Face _horizontalDatumFace,Snap.NX.Body body)
    {
        if (!toggle0.Value) 
        {
            body.Faces.ToList().ForEach(u =>
            {
                if (u.GetAttributeInfo().Where(m => m.Title == "EACT_ELECT_X_FACE").Count() > 0)
                {
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE");
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE");
                    u.SetStringAttribute("EACT_ELECT_Y_FACE", "1");
                }
                else if (u.GetAttributeInfo().Where(m => m.Title == "EACT_ELECT_Y_FACE").Count() > 0)
                {
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE");
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE");
                    u.SetStringAttribute("EACT_ELECT_X_FACE", "1");
                }
            });
        }
    }

    void RefreshUI() 
    {
        //var isShow = toggleIsMove.Value;
        //groupXYZ.Show = isShow;
        var isShow = bodySelect0.GetSelectedObjects().Count() <= 1;
        toggleJiaju.Show = isShow;
        selectionJiaju.Show = isShow&&toggleJiaju.Value;
    }

    /// <summary>
    /// 获取最大序号
    /// </summary>
    int GetMaxOrder() 
    {
        var root = Snap.Globals.WorkPart.RootComponent;
        var list = new List<int>();

        string str = string.Empty;
        GetElecNames().ForEach(u =>
        {
            int order;
            if (int.TryParse(u.Split('-').Last(), out order))
            {
                list.Add(order);
            }
        });

        return list.Max();

       
    }

    string GetElecName(double order) 
    {
        var body = bodySelect0.GetSelectedObjects().FirstOrDefault() as NXOpen.Body;
        string selectedComName = body.OwningComponent == null ? body.Name : body.OwningComponent.Name;
        var name = string.Empty;
        var strList = selectedComName.Split('-').ToList();
        for (int i = 0; i < strList.Count - 1; i++)
        {
            name += strList[i] + '-';
        }

        string lastStr= strList.LastOrDefault();
        int num = lastStr.Length;

        var result = order + 1;
        var orderStr = result.ToString();
        num = num - orderStr.Length;
        for (int i = 0; i < num; i++) 
        {
            orderStr = string.Format("0{0}", orderStr);
        }

        return name + orderStr;
    }

    /// <summary>
    /// 设置电极名称
    /// </summary>
    void SetElecName() 
    {
        var name= GetElecName(GetMaxOrder());
        stringElecName.Value =name;
    }
    List<string> GetElecNames() 
    {
        var root = Snap.Globals.WorkPart.RootComponent;
        var names = new List<string>();
        if (root != null)
        {
            root.Children.ToList().ForEach(u => { names.Add(u.Name); });
        }

        Snap.Globals.WorkPart.Bodies.Where(u => SnapEx.Helper.IsEactElecBody(u)).ToList().ForEach(u =>
        {
            names.Add(u.Name);
        });
        return names;
    }

}