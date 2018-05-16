using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snap;
public partial class EleAutoCamUI:SnapEx.BaseUI
{
    //几何组根节点
    NXOpen.Tag geometryGroupRootTag;

    //程序组根节点
    NXOpen.Tag orderGroupRootTag;

    //刀具组根节点
    NXOpen.Tag cutterGroupRootTag;

    //方法组根节点
    NXOpen.Tag methodGroupRootTag;

    //几何体组
    NXOpen.Tag workGeometryGroupTag;

    //加工坐标系
    NXOpen.Tag workMcsGroupTag;

    /// <summary>
    /// 模板名称
    /// </summary>
    string templateTypeName;

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        
    }

    public override void Apply()
    {
        EleAutoCam.NxopenCAM.CreateCAM();
        return;
        var ufSession = NXOpen.UF.UFSession.GetUFSession();

        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;
        Part displayPart = theSession.Parts.Display;

        Snap.NX.Part snapPart = workPart;
        double[] param = new double[2], point = new double[3], u1 = new double[3], v1 = new double[3], u2 = new double[3], v2 = new double[3], unitNorm = new double[3], radii = new double[2];

        //分析几何体，得到平面，直面，斜面，曲面,并对其分类着色
        snapPart.Bodies.First().Faces.ToList().ForEach(u =>
        {
            ufSession.Modl.AskFaceProps(u.NXOpenTag, param, point, u1, v1, u2, v2, unitNorm, radii);
            var unitNormAbs0 = System.Math.Abs(unitNorm[0]);
            var unitNormAbs1 = System.Math.Abs(unitNorm[1]);
            var unitNormAbs2 = System.Math.Abs(unitNorm[2]);
            switch (u.NXOpenFace.SolidFaceType)
            {
                case Face.FaceType.Planar:
                    {
                        if (0.00001 > unitNormAbs0 && 0.00001 > unitNormAbs1 && 0.99999 < unitNorm[2])//平面颜色
                        {
                            u.Color = System.Drawing.Color.Blue;
                        }
                        else if (0.00001 < unitNorm[2] && unitNorm[2] < 1)//斜面颜色
                        {
                            u.Color = System.Drawing.Color.Red;
                        }
                        else if (0.00001 > unitNormAbs2)//直面颜色
                        {
                            u.Color = System.Drawing.Color.Brown;
                        }
                        else if (-0.00001 > unitNorm[2] && unitNorm[2] > -1)//无法加工的面
                        {
                            u.Color = System.Drawing.Color.BurlyWood;
                        }
                        else if (unitNorm[2] < -0.99999)//无法加工的面
                        {
                            u.Color = System.Drawing.Color.CadetBlue;
                        }
                        break;
                    }
                default:
                    {
                        if (0.00001 < unitNorm[2]) //曲面
                        {
                            u.Color = System.Drawing.Color.Aqua;
                        }
                        else if (0.00001 > unitNormAbs2)//直面
                        {
                            u.Color = System.Drawing.Color.Beige;
                        }
                        else if (-0.00001 > unitNormAbs2)//无法加工的面
                        {
                            u.Color = System.Drawing.Color.Bisque;
                        }
                        break;
                    }
            }
        });
        templateTypeName = "ElectrodeAutoCam";

        //进入CAM模块
        theSession.CreateCamSession();
        //初始化CAM
        var cAMSetup1 = workPart.CreateCamSetup(templateTypeName);

        //TODO 初始化对象
        NXOpen.Tag setup_tag;
        ufSession.Setup.AskSetup(out setup_tag);
        ufSession.Setup.AskGeomRoot(setup_tag, out geometryGroupRootTag);
        ufSession.Setup.AskProgramRoot(setup_tag, out orderGroupRootTag);
        ufSession.Setup.AskMctRoot(setup_tag, out cutterGroupRootTag);
        ufSession.Setup.AskMthdRoot(setup_tag, out methodGroupRootTag);

        //TODO 创建坐标系和几何体
        NXOpen.Tag workMcsGroupTag;
        ufSession.Ncgeom.Create("mill_planar", "MCS", out workMcsGroupTag);
        ufSession.Obj.SetName(workMcsGroupTag, "GEOM_PH");
        ufSession.Ncgroup.AcceptMember(geometryGroupRootTag, workMcsGroupTag);

        //TODO 设置安全平面
        var normal = new Snap.Vector(0, 0, 1);
        var origin = new Snap.Position();
        ufSession.Cam.SetClearPlaneData(workMcsGroupTag, origin.Array, normal.Array);

        //TODO 创建几何体
        ufSession.Ncgeom.Create(templateTypeName, "WORKPIECE", out workGeometryGroupTag);
        ufSession.Obj.SetName(workGeometryGroupTag, "WORKPIECE_PH");
        ufSession.Ncgroup.AcceptMember(workMcsGroupTag, workGeometryGroupTag);

        //TODO 添加Body作为工作几何体
        SnapEx.CAMEx.SetMillArea(NXOpen.UF.CamGeomType.CamPart, workGeometryGroupTag, new List<Tag> { workPart.Bodies.ToArray().First().Tag });

        //TODO 设置毛坯为自动块
        ufSession.Cam.SetAutoBlank(workGeometryGroupTag, NXOpen.UF.UFCam.BlankGeomType.AutoBlockType, new double[] { 0, 0, 0, 0, 0, 0 });

        //TODO 创建程序
        NXOpen.Tag programGroupTag;
        ufSession.Ncprog.Create(templateTypeName, "PROGRAM", out programGroupTag);
        ufSession.Obj.SetName(programGroupTag, "PROGRAM_PH");
        ufSession.Ncgroup.AcceptMember(orderGroupRootTag, programGroupTag);

        //TODO 创建Cut,并设置相应的参数
        NXOpen.Tag cutterTag;
        ufSession.Cutter.Create(templateTypeName, "MILL", out cutterTag);
        ufSession.Ncgroup.AcceptMember(cutterGroupRootTag, cutterTag);
        ufSession.Obj.SetName(cutterTag, "PH_TOOL1");
        ufSession.Param.SetDoubleValue(cutterTag, NXOpen.UF.UFConstants.UF_PARAM_TL_DIAMETER, 6);


        //TODO 创建工序
        NXOpen.Tag operTag;
        ufSession.Oper.Create(templateTypeName, "FACE_MILLING_KC", out operTag);//铣顶面
        ufSession.Ncgroup.AcceptMember(workGeometryGroupTag, operTag);
        ufSession.Ncgroup.AcceptMember(programGroupTag, operTag);
        ufSession.Ncgroup.AcceptMember(methodGroupRootTag, operTag);
        ufSession.Ncgroup.AcceptMember(cutterTag, operTag);

        //TODO 指定面边界
        var electrode = ElecManage.XKElectrode.GetElectrode(snapPart.Bodies.First());
        if (electrode != null && electrode.ElecHeadFaces.Count > 0)
        {
            var headFace = electrode.ElecHeadFaces.OrderBy(u => Snap.Compute.Distance(new Snap.Position(), u)).FirstOrDefault();
            SnapEx.CAMEx.SetBoundaryByFace(NXOpen.UF.CamGeomType.CamBlank, operTag, headFace.NXOpenTag, NXOpen.UF.CamMaterialSide.CamMaterialSideInLeft);

            SnapEx.CAMEx.PathGenerate(new List<Tag> { operTag });
        }
    }

}