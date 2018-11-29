using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

partial class BatchGeomcopyElecUI : SnapEx.BaseUI
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
        snapSelectJiaju.AllowMultiple = true;

        tempObjs.Clear();
        RefreshUI();
    }

    public override void DialogShown()
    {
        expressionX.Value = 0;
        expressionY.Value = 0;
        expressionZ.Value = 0;
        toggleJiaju.Value = false;
        togglePreview.Value = false;
        stringElecName.Show = false;
    }
    public override void Apply()
    {
        Perform();
        tempObjs.Clear();
    }


    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == bodySelect0.NXOpenBlock)
        {
            var selectedObjs = bodySelect0.SelectedObjects.ToList();
            var bodies = new List<Snap.NX.NXObject>();

            selectedObjs.ForEach(u =>
            {
                var body = Snap.NX.Body.Wrap(u.NXOpenTag);
                if (SnapEx.Helper.IsEactElecBody(body))
                {
                    bodies.Add(body);
                }
            });

            if (selectedObjs.Count != bodies.Count)
            {
                bodySelect0.SelectedObjects = bodies.ToArray();
            }

            //stringElecName.Show = bodies.Count <= 1;


            //if (bodies.Count == 1)
            //{
            //    SetElecName();
            //}
        }
        else if (block == selectionJiaju.NXOpenBlock) 
        {
            var selectedObjs = selectionJiaju.SelectedObjects.ToList();
            var bodies = new List<Snap.NX.NXObject>();

            selectedObjs.ForEach(u =>
            {
                var body = Snap.NX.Body.Wrap(u.NXOpenTag);
                if (!SnapEx.Helper.IsEactElecBody(body))
                {
                    bodies.Add(body);
                }
            });

            selectionJiaju.SelectedObjects = bodies.ToArray();
        }
        //else if (block == stringElecName)
        //{
        //    var body = bodySelect0.GetSelectedObjects().FirstOrDefault() as NXOpen.Body;
        //    if (body != null)
        //    {
        //        if (GetElecNames().Contains(stringElecName.Value))
        //        {
        //            SetElecName();
        //        }
        //    }
        //}


        RefreshUI();
        Perform(true);
    }

    List<NXOpen.DisplayableObject> tempObjs = new List<NXOpen.DisplayableObject>();
    void Perform(bool isPreview = false)
    {
        tempObjs.ForEach(u =>
        {
            Snap.NX.NXObject snapU = u;
            snapU.Delete();
        });
        tempObjs.Clear();
        if (!togglePreview.Value&&isPreview) return;
        var bodies = bodySelect0.SelectedObjects.ToList();
        var plane = plane0.SpecifiedPlane;
        if (bodies.Count > 0 && plane != null)
        {
            //int order = GetMaxOrder();
            //if (bodies.Count == 1)
            //{
            //    int tempInt;
            //    if (int.TryParse(stringElecName.Value.Split('-').LastOrDefault(), out tempInt))
            //    {
            //        order = tempInt - 1;
            //    }
            //}
            bodies.ForEach(u =>
            {
                //order++;
                tempObjs.Add(GeomcopyElec(isPreview, Snap.NX.Body.Wrap(u.NXOpenTag).NXOpenBody, plane.NXOpenDisplayableObject as NXOpen.Plane
                    //, order
                    ));
            });

            GeomcopyJiaju(isPreview, plane.NXOpenDisplayableObject as NXOpen.Plane);
        }

        if (isPreview)
        {
            SnapEx.Create.DisplayModification(tempObjs);
        }
    }

    void GeomcopyJiaju(bool isPreview, NXOpen.Plane plane) 
    {
        if (!isPreview) 
        {
            if (toggleJiaju.Value)
            {
                var x = expressionX.Value;
                var y = expressionY.Value;
                var z = expressionZ.Value;
                var trans = Snap.Geom.Transform.CreateTranslation(new Snap.Vector(x, y, z));
                selectionJiaju.SelectedObjects.ToList().ForEach(u => {
                    Snap.NX.Body snapU = Snap.NX.Body.Wrap(u.NXOpenTag);
                    if (snapU != null)
                    {
                        var jiaju = snapU.Copy();
                        var firstPos = new Snap.Position((jiaju.Box.MinX + jiaju.Box.MaxX) / 2, (jiaju.Box.MinY + jiaju.Box.MaxY) / 2, (jiaju.Box.MinZ + jiaju.Box.MaxZ) / 2);
                        var twoPos = firstPos.Copy(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
                        jiaju.Move(Snap.Geom.Transform.CreateTranslation(twoPos - firstPos));
                        //if (toggle0.Value) //继承
                        //{
                        //    var firstPos = new Snap.Position((jiaju.Box.MinX + jiaju.Box.MaxX) / 2, (jiaju.Box.MinY + jiaju.Box.MaxY) / 2, (jiaju.Box.MinZ + jiaju.Box.MaxZ) / 2);
                        //    var twoPos = firstPos.Copy(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
                        //    jiaju.Move(Snap.Geom.Transform.CreateTranslation(twoPos - firstPos));
                        //}
                        //else 
                        //{
                        //    jiaju.Move(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
                        //}
                        jiaju.SetStringAttribute("EACT_MIRROR_ELECT", "Y");
                        jiaju.Move(trans);
                        snapU.Delete();
                        
                       
                        
                    }
                });
            }
        }
    }

    NXOpen.DisplayableObject GeomcopyElec(bool isPreview, NXOpen.Body body, NXOpen.Plane plane)
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
            newBody = SnapBody.Copy();
        }

        if (newBody == null) return newBody;

        var guid = Guid.NewGuid().ToString();

        var tempElecOrigin = SnapEx.Helper.GetElecMidPosition(oldWorkPart, body);
        if (tempElecOrigin == null)
        {
            theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, "该电极未发现基准点！");
            return null;
        }

        var transRef = Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal));
        var topFace = SnapEx.Helper.GetTopFace(newBody);
        var bottomFace = SnapEx.Helper.GetBottomFace(newBody);
        var minU = topFace.BoxUV.MinU;
        var minV = topFace.BoxUV.MinV;
        var maxU = topFace.BoxUV.MaxU;
        var maxV = topFace.BoxUV.MaxV;
        var centerPosition = (Snap.Position)tempElecOrigin;
        var movePosition = centerPosition.Copy(transRef);

        bool isJC = toggle0.Value && topFace.NXOpenTag != bottomFace.NXOpenTag;
        if (isJC) //继承
        {
            var markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "reflectionBody");
            var normal = topFace.Normal(minU, minV);
            var point = topFace.Position(minU, minV);

            var datumPlane = Snap.Create.DatumPlane(point, normal);
            var splitBody = Snap.Create.SplitBody(newBody, datumPlane);
            var topBody = splitBody.Bodies.FirstOrDefault(b => SnapEx.Helper.GetTopFace(b) == null).Copy();
            var reflectionBody = splitBody.Bodies.FirstOrDefault(b => SnapEx.Helper.GetTopFace(b) != null).Copy();
            splitBody.Delete();
            newBody.Delete();
            topBody.Move(transRef);
            reflectionBody.Move(Snap.Geom.Transform.CreateTranslation(movePosition - centerPosition));
            var result = Snap.Create.Unite(reflectionBody, topBody);
            result.Orphan();

            datumPlane.Delete();

            theSession.UpdateManager.DoUpdate(markId1);
            theSession.DeleteUndoMark(markId1, null);

            newBody = reflectionBody;
        }
        else
        {
            newBody.Move(transRef);
        }

        var x = expressionX.Value;
        var y = expressionY.Value;
        var z = expressionZ.Value;
        var trans = Snap.Geom.Transform.CreateTranslation(new Snap.Vector(x, y, z));
        newBody.Move(trans);
        if (!isPreview)
        {
            newBody.SetStringAttribute("EACT_MIRROR_ELECT", "Y");
            if (topFace != null)
            {
                //SetJZJAttr(topFace, newBody);
                SetGeomcopyAttr(newBody,plane, isJC);
            }
            var oldPoint=SnapEx.Helper.GetElecMidPointInPart(Snap.Globals.WorkPart, body);
            var newPoint = SnapEx.Helper.GetElecMidPoint(Snap.Globals.WorkPart, body);

            if (newPoint != null)
            {
                //newPoint.SetStringAttribute(SnapEx.EactConstString.EACT_ELECT_GROUP, guid);
                newPoint.Move(Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal)));
                newPoint.Move(trans);
            }

            if (body.OwningComponent != null && body.Prototype != null)
            {
                
            }
            else 
            {
                if (oldPoint != null) { oldPoint.Delete(); }
                SnapBody.Delete();
            }
        }

        return newBody;
    }

    /// <summary>
    /// 设置基准角相关属性
    /// </summary>
    void SetJZJAttr(Snap.NX.Face _horizontalDatumFace, Snap.NX.Body body)
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

    /// <summary>
    /// 设置镜像属性
    /// </summary>
    void SetGeomcopyAttr(Snap.NX.Body body, NXOpen.Plane plane, bool isJC)
    {
        if (isJC) return;
        var electrode = ElecManage.Electrode.GetElectrode(body);
        if (electrode == null) return;
        electrode.InitAllFace();
        //var transRef = Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal));

        Snap.NX.Face preXFace = null, preYFace = null;

        body.Faces.ToList().ForEach(u =>
        {
            if (u.GetAttributeInfo().Where(m => m.Title == "EACT_ELECT_X_FACE").Count() > 0)
            {
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE");
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE");
                preXFace = u;
            }
            else if (u.GetAttributeInfo().Where(m => m.Title == "EACT_ELECT_Y_FACE").Count() > 0)
            {
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE");
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE");
                preYFace = u;
            }
        });

        //if (preXFace == null || preYFace == null) return;
        var topDir = -electrode.BaseFace.GetFaceDirection();
        var orientation = ElecManage.Electrode.GetSidelongOrientation(new Snap.Orientation(topDir));
        var xFace = electrode.BaseSideFaces.FirstOrDefault(u => SnapEx.Helper.Equals(orientation.AxisX, u.GetFaceDirection()));
        var yFace = electrode.BaseSideFaces.FirstOrDefault(u => SnapEx.Helper.Equals(orientation.AxisY, u.GetFaceDirection()));
        if (xFace != null && yFace != null)
        {
            xFace.SetStringAttribute("EACT_ELECT_X_FACE", "1");
            yFace.SetStringAttribute("EACT_ELECT_Y_FACE", "1");
        }

        body.SetStringAttribute("EACT_EDM_OPERATE_DIR", ElecManage.Electrode.GetDIRECTION(topDir));
        body.SetStringAttribute("EACT_ELEC_QUADRANT_OF_CHAMFER", ((int)electrode.GetQuadrantType(orientation) + 1).ToString());
    }

    void RefreshUI()
    {
        //var isShow = toggleIsMove.Value;
        //groupXYZ.Show = isShow;
        //var isShow = bodySelect0.GetSelectedObjects().Count() <= 1;
        //toggleJiaju.Show = isShow;
        //selectionJiaju.Show = isShow && toggleJiaju.Value;
        selectionJiaju.Show = toggleJiaju.Value;
    }

    ///// <summary>
    ///// 获取最大序号
    ///// </summary>
    //int GetMaxOrder()
    //{
    //    var root = Snap.Globals.WorkPart.RootComponent;
    //    var list = new List<int>();

    //    string str = string.Empty;
    //    GetElecNames().ForEach(u =>
    //    {
    //        int order;
    //        if (int.TryParse(u.Split('-').Last(), out order))
    //        {
    //            list.Add(order);
    //        }
    //    });

    //    return list.Max();


    //}

    //string GetElecName(double order)
    //{
    //    var body = bodySelect0.GetSelectedObjects().FirstOrDefault() as NXOpen.Body;
    //    string selectedComName = body.OwningComponent == null ? body.Name : body.OwningComponent.Name;
    //    var name = string.Empty;
    //    var strList = selectedComName.Split('-').ToList();
    //    for (int i = 0; i < strList.Count - 1; i++)
    //    {
    //        name += strList[i] + '-';
    //    }

    //    string lastStr = strList.LastOrDefault();
    //    int num = lastStr.Length;

    //    var result = order + 1;
    //    var orderStr = result.ToString();
    //    num = num - orderStr.Length;
    //    for (int i = 0; i < num; i++)
    //    {
    //        orderStr = string.Format("0{0}", orderStr);
    //    }

    //    return name + orderStr;
    //}

    /// <summary>
    /// 设置电极名称
    /// </summary>
    //void SetElecName()
    //{
    //    var name = GetElecName(GetMaxOrder());
    //    stringElecName.Value = name;
    //}
    //List<string> GetElecNames()
    //{
    //    var root = Snap.Globals.WorkPart.RootComponent;
    //    var names = new List<string>();
    //    if (root != null)
    //    {
    //        root.Children.ToList().ForEach(u => { names.Add(u.Name); });
    //    }

    //    Snap.Globals.WorkPart.Bodies.Where(u => SnapEx.Helper.IsEactElecBody(u)).ToList().ForEach(u =>
    //    {
    //        names.Add(u.Name);
    //    });
    //    return names;
    //}

}