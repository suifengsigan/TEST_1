﻿using System;
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

            selectedObjs.ForEach(u =>
            {
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
    void Perform(bool isPreview = false)
    {
        tempObjs.ForEach(u =>
        {
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
                tempObjs.Add(GeomcopyElec(isPreview, u as NXOpen.Body, plane, order));
            });
        }

        if (isPreview)
        {
            SnapEx.Create.DisplayModification(tempObjs);
        }
    }

    NXOpen.DisplayableObject GeomcopyElec(bool isPreview, NXOpen.Body body, NXOpen.Plane plane, int order)
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

        var tempElecOrigin = SnapEx.Helper.GetElecMidPosition(oldWorkPart, body);
        if (tempElecOrigin == null)
        {
            theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, "该电极未发现基准点！");
            return null;
        }

        var guid = Guid.NewGuid().ToString();

        //TODO 修改名称 修改属性
        newBody.Name = GetElecName(order - 1);
        newBody.SetStringAttribute("EACT_ELEC_NAME", newBody.Name);
        newBody.SetStringAttribute("EACT_ELECT_GROUP", guid);
        newBody.Layer = order;
        Snap.Globals.LayerStates[order] = isPreview ? Snap.Globals.LayerState.Visible : Snap.Globals.LayerState.Selectable;

        var transRef = Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal));
        var topFace = SnapEx.Helper.GetTopFace(newBody);
        var bottomFace = SnapEx.Helper.GetBottomFace(newBody);
        var minU = topFace.BoxUV.MinU;
        var minV = topFace.BoxUV.MinV;
        var maxU = topFace.BoxUV.MaxU;
        var maxV = topFace.BoxUV.MaxV;
        var centerPosition =(Snap.Position) tempElecOrigin;
        var movePosition = centerPosition.Copy(transRef);

        if (toggle0.Value && topFace.NXOpenTag!=bottomFace.NXOpenTag) //继承
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
            topFace = SnapEx.Helper.GetTopFace(newBody);
            if (topFace != null)
            {
                //SetJZJAttr(topFace, newBody);
                SetGeomcopyAttr(newBody, plane);
            }
            var newPoint = SnapEx.Helper.GetElecMidPoint(Snap.Globals.WorkPart, body);
            if (newPoint != null)
            {
                newPoint.SetStringAttribute(SnapEx.EactConstString.EACT_ELECT_GROUP, guid);
                newPoint.Layer = order;
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
                    jiaju.Move(Snap.Geom.Transform.CreateTranslation(movePosition - centerPosition));
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
                }
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

    void RefreshUI()
    {
        //var isShow = toggleIsMove.Value;
        //groupXYZ.Show = isShow;
        var isShow = bodySelect0.GetSelectedObjects().Count() <= 1;
        toggleJiaju.Show = isShow;
        selectionJiaju.Show = isShow && toggleJiaju.Value;
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

        var result = 0;
        if (list.Count > 0) 
        {
            result = list.Max();
        }

        return result;


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

        string lastStr = strList.LastOrDefault();
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
        var name = GetElecName(GetMaxOrder());
        stringElecName.Value = name;
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


    /// <summary>
    /// 设置镜像属性
    /// </summary>
    List<Snap.NX.Face> SetGeomcopyAttr(Snap.NX.Body body, NXOpen.Plane plane)
    {
        var orientation = Snap.Orientation.Identity;
        var baseFace = SnapEx.Helper.GetBottomFace(body);
        var faces = new List<Snap.NX.Face>();
        if (baseFace != null)
        {
            var tempFaces = body.Faces.Where(u => !u.NXOpenTag.Equals(baseFace.NXOpenTag)
                    && Equals(baseFace.Box.MinZ, u.Box.MaxZ)).ToList();
            faces.AddRange(tempFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -orientation.AxisY)).OrderByDescending(u => u.Area).Take(1));
            faces.AddRange(tempFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), orientation.AxisX)).OrderByDescending(u => u.Area).Take(1));
            faces.AddRange(tempFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), orientation.AxisY)).OrderByDescending(u => u.Area).Take(1));
            faces.AddRange(tempFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -orientation.AxisX)).OrderByDescending(u => u.Area).Take(1));
        }

        Snap.NX.Face preXFace = null, preYFace = null;

        body.Faces.ToList().ForEach(u =>
        {
            if (u.GetAttributeInfo().Where(m => m.Title == "EACT_ELECT_X_FACE").Count() > 0)
            {
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE");
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE");
                //u.SetStringAttribute("EACT_ELECT_Y_FACE", "1");
                preXFace = u;
            }
            else if (u.GetAttributeInfo().Where(m => m.Title == "EACT_ELECT_Y_FACE").Count() > 0)
            {
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_X_FACE");
                u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, "EACT_ELECT_Y_FACE");
                //u.SetStringAttribute("EACT_ELECT_X_FACE", "1");
                preYFace = u;
            }
        });

        if (preXFace != null && preYFace != null)
        {
            var transRef = Snap.Geom.Transform.CreateReflection(new Snap.Geom.Surface.Plane(plane.Origin, plane.Normal));
            var dir = preXFace.GetFaceDirection().Copy(transRef) + preYFace.GetFaceDirection().Copy(transRef);
            dir = dir.Copy(transRef);
            if (dir.Y > 0 && dir.X > 0)
            {
                body.SetStringAttribute("EACT_ELEC_QUADRANT_OF_CHAMFER", "3");
            }
            else if (dir.Y > 0 && dir.X < 0)
            {
                body.SetStringAttribute("EACT_ELEC_QUADRANT_OF_CHAMFER", "4");
            }
            else if (dir.Y < 0 && dir.X < 0)
            {
                body.SetStringAttribute("EACT_ELEC_QUADRANT_OF_CHAMFER", "1");
            }
            else
            {
                body.SetStringAttribute("EACT_ELEC_QUADRANT_OF_CHAMFER", "2");
            }
        }

        var xFace = faces.FirstOrDefault(u => SnapEx.Helper.Equals(orientation.AxisX, u.GetFaceDirection()));
        var yFace = faces.FirstOrDefault(u => SnapEx.Helper.Equals(orientation.AxisY, u.GetFaceDirection()));
        if (xFace != null && yFace != null)
        {
            xFace.SetStringAttribute("EACT_ELECT_X_FACE", "1");
            yFace.SetStringAttribute("EACT_ELECT_Y_FACE", "1");
        }

        return faces;
    }

}