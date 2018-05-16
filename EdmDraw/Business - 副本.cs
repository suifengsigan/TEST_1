using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;
using NXOpen.Drawings;
using NXOpen;

partial class EdmDrawUI 
{
    List<string> _paramFileList = new List<string>();
    void Init() 
    {
        var members=new List<string>();
        var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template");
        if (System.IO.Directory.Exists(path)) 
        {
            _paramFileList = System.IO.Directory.GetFiles(path).ToList();
            _paramFileList.ForEach(u =>
            {
                members.Add(System.IO.Path.GetFileNameWithoutExtension(u));
            });
        }

        selectTemplate0.SetEnumMembers(members.ToArray());
    }
    void Apply() 
    {
        Snap.NX.Body selectedObj = bodySelect0.GetSelectedObjects().FirstOrDefault() as NXOpen.Body;
        var workPart=Snap.Globals.WorkPart;
        selectedObj.SetStringAttribute("2323", "3333");
        var templateName = _paramFileList.Where(u => u.Contains(selectTemplate0.ValueAsString)).FirstOrDefault();

        //新建图纸页
        var ds = SnapEx.Create.DrawingSheet("test_dds", templateName);
        
        //var baseView = SnapEx.Create.BaseView(Snap.Globals.WorkPart, new NXOpen.Point2d(expressionX.Value, expressionY.Value));
        CreateBaseView(ds,selectedObj);

        //解决位置固定问题
        var topView1 = CreateView(ds, Snap.Globals.WorkPart.Bodies.Where(u => u.NXOpenTag != selectedObj.NXOpenTag).FirstOrDefault(), workPart.NXOpenPart.ModelingViews.FindObject("TOP").Tag, new Snap.Position(40,60));
        var size=GetBorderSize(topView1);
        var theUFSession = NXOpen.UF.UFSession.GetUFSession();
        theUFSession.Draw.SetViewScale(topView1,40/size.X);
        theUFSession.Draw.UpdateOneView(ds.Tag, topView1);

        Test(ds);
    }


    public void Test(DrawingSheet ds) 
    {
        var workPart = Snap.Globals.WorkPart;
        var baseView=CreateTextView(ds,workPart.NXOpenPart.ModelingViews.FindObject("FRONT").Tag, new Snap.Position(110,140));

        var point = workPart.Points.FirstOrDefault();
        var theUFSession=NXOpen.UF.UFSession.GetUFSession();

        //创建坐标标注的原点
        NXOpen.UF.UFDrf.Object object1 = new NXOpen.UF.UFDrf.Object();

        object1.object_tag =point.NXOpenTag;
        object1.object_view_tag = baseView;
        object1.object_assoc_type = NXOpen.UF.UFDrf.AssocType.EndPoint;
        object1.object_assoc_modifier = NXOpen.UF.UFConstants.UF_DRF_first_end_point;

        var orginTag=NXOpen.Tag.Null;
        theUFSession.Drf.CreateOrdorigin(ref object1, 1, 1, 2, "test", out orginTag);

        //创建坐标标注的尺寸边缘(留边)
        NXOpen.UF.UFDrf.Object object2 = new NXOpen.UF.UFDrf.Object();
        Tag top = Tag.Null;
        theUFSession.Obj.CycleByName("TOPEDGE", ref top);
        object2.object_tag = top;
        object2.object_view_tag = baseView;
        object2.object_assoc_type = NXOpen.UF.UFDrf.AssocType.EndPoint;
        object2.object_assoc_modifier = NXOpen.UF.UFConstants.UF_DRF_first_end_point;

        var marginTag=NXOpen.Tag.Null;
        theUFSession.Drf.CreateOrdmargin(1, orginTag, ref object2, null, null, 0, out marginTag);


        NXOpen.UF.UFDrf.Object object3 = new NXOpen.UF.UFDrf.Object();
        object3.object_tag = point.NXOpenTag;
        object3.object_view_tag = baseView;
        object3.object_assoc_type = NXOpen.UF.UFDrf.AssocType.EndPoint;
        object3.object_assoc_modifier = NXOpen.UF.UFConstants.UF_DRF_first_end_point;

        NXOpen.UF.UFDrf.Text text = new NXOpen.UF.UFDrf.Text();
        double[] origin3d = { 10, 10, 0 };
        NXOpen.Tag result;
        theUFSession.Drf.CreateOrddimension(marginTag, 1, ref object3, 0, 50, ref text, 2, origin3d, out result);
    }

    public NXOpen.Tag CreateTextView(DrawingSheet ds, NXOpen.Tag modelViewTag, Snap.Position point) 
    {
        //UFUN创建基本视图函数  
        var workPart = NXOpen.Session.GetSession().Parts.Work;
        Snap.NX.Part snapWorkPart = workPart;
        var theUFSession = NXOpen.UF.UFSession.GetUFSession();
        NXOpen.UF.UFDraw.ViewInfo view_info;
        theUFSession.Draw.InitializeViewInfo(out view_info);
        double[] dwg_point = { point.X, point.Y };
        Tag draw_view_tag;
        theUFSession.Draw.ImportView(ds.Tag, modelViewTag, dwg_point, ref view_info, out draw_view_tag);
        string viewName;
        theUFSession.Obj.AskName(draw_view_tag, out viewName);
        theUFSession.Draw.UpdateOneView(ds.Tag, draw_view_tag);
        return draw_view_tag;
    }

    public static NXOpen.Tag CreateView(DrawingSheet ds,Snap.NX.Body body,NXOpen.Tag modelViewTag,Snap.Position point) 
    {
        //UFUN创建基本视图函数  
        var workPart = NXOpen.Session.GetSession().Parts.Work;
        Snap.NX.Part snapWorkPart = workPart;
        var theUFSession = NXOpen.UF.UFSession.GetUFSession();
        NXOpen.UF.UFDraw.ViewInfo view_info;
        theUFSession.Draw.InitializeViewInfo(out view_info);
        double[] dwg_point = { point.X,point.Y};
        Tag draw_view_tag;
        theUFSession.Draw.ImportView(ds.Tag, modelViewTag, dwg_point, ref view_info, out draw_view_tag);
        string viewName;
        theUFSession.Obj.AskName(draw_view_tag, out viewName);

        workPart.Bodies.ToArray().ToList().ForEach(u =>
        {
            if (u.Tag != body.NXOpenTag)
            {
                SnapEx.Ex.UC6400(viewName, u.Tag);
            }

        });

        workPart.Points.ToArray().ToList().ForEach(u =>
        {
            if (u.Tag != body.NXOpenTag)
            {
                SnapEx.Ex.UC6400(viewName, u.Tag);
            }

        });

        SnapEx.Ex.UC6400(viewName, workPart.WCS.Tag);

        //更新视图
        theUFSession.Draw.UpdateOneView(ds.Tag, draw_view_tag);

        theUFSession.Draw.MoveView(draw_view_tag, dwg_point);
        //更新视图
        theUFSession.Draw.UpdateOneView(ds.Tag, draw_view_tag);
        return draw_view_tag;
    }


    /// <summary>
    /// 创建基本视图
    /// </summary>
    /// <returns></returns>
    public static BaseView CreateBaseView(DrawingSheet ds,Snap.NX.Body body)
    {
        //UFUN创建基本视图函数  
        var workPart = NXOpen.Session.GetSession().Parts.Work;
        var theUFSession = NXOpen.UF.UFSession.GetUFSession();
        var modelView = workPart.ModelingViews.FindObject("FRONT");
        var draw_view_tag = CreateView(ds, body, modelView.Tag, new Snap.Position(ds.Length / 2, ds.Height / 2));


        Tag top = Tag.Null;
        var topFace=body.Faces.FirstOrDefault(u => u.GetAttributeInfo().Where(m => m.Title == "ATTR_NAME_MARK" && u.GetStringAttribute("ATTR_NAME_MARK") == "BASE_BOT").Count() > 0);
        top = topFace.NXOpenTag;

        var box = body.Box;
        var min = new Snap.Position((box.MinX + box.MaxX) / 2, (box.MinY + box.MaxY) / 2, box.MinZ);
        var plane=new Snap.Geom.Surface.Plane(min, topFace.GetFaceDirection());
        var bottomFace =
        body.Faces.OrderBy(u => 
            Snap.Compute.Distance(min, u)
        ).FirstOrDefault();

        Tag bottom = Tag.Null;
        //theUFSession.Obj.CycleByName("BOTTOMEDGE", ref bottom);
        bottom = bottomFace.NXOpenTag;

        CreateVerticalDim(draw_view_tag, top, bottom, new Snap.Position((ds.Length / 2) + GetBorderSize(draw_view_tag).X/2, ds.Height / 2, 0));

        return null;
    }

    /// <summary>
    ///  获取边界尺寸
    /// </summary>
    public static NXOpen.Point2d GetBorderSize(NXOpen.Tag tag)
    {
        var view_borders = new double[4];
        NXOpen.UF.UFSession.GetUFSession().Draw.AskViewBorders(tag, view_borders);
        var size = new NXOpen.Point2d();
        size.Y = Math.Abs(view_borders[3] - view_borders[1]);
        size.X = Math.Abs(view_borders[2] - view_borders[0]);
        return size;
    }

    public static NXOpen.Tag CreateVerticalDim(NXOpen.Tag draw_view_tag, NXOpen.Tag o1, NXOpen.Tag o2, Snap.Position origin)
    {
        var theUFSession = NXOpen.UF.UFSession.GetUFSession();
        NXOpen.UF.UFDrf.Object object1 = new NXOpen.UF.UFDrf.Object();

        object1.object_tag = o1;
        object1.object_view_tag = draw_view_tag;
        object1.object_assoc_type = NXOpen.UF.UFDrf.AssocType.EndPoint;
        object1.object_assoc_modifier = NXOpen.UF.UFConstants.UF_DRF_first_end_point;

        NXOpen.UF.UFDrf.Object object2 = new NXOpen.UF.UFDrf.Object();
        object2.object_tag = o2;
        object2.object_view_tag = draw_view_tag;
        object2.object_assoc_type = NXOpen.UF.UFDrf.AssocType.EndPoint;
        object2.object_assoc_modifier = NXOpen.UF.UFConstants.UF_DRF_first_end_point;

        NXOpen.UF.UFDrf.Text text = new NXOpen.UF.UFDrf.Text();

        NXOpen.Tag result;
        theUFSession.Drf.CreateVerticalDim(ref object1, ref object2, ref text, new double[] { origin.X, origin.Y, 0 }, out result);

        return result;
    }
}
