using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleAutoCam
{
    public class NxopenCAM
    {
        public static void CreateCAM()
        {
            var theSession = Session.GetSession();
            var workPart = theSession.Parts.Work;
            var displayPart = theSession.Parts.Display;

            //创建加工会话
            var result1 = theSession.IsCamSessionInitialized();
            theSession.CreateCamSession();

            var kinematicConfigurator1 = workPart.CreateKinematicConfigurator();

            var cAMSetup1 = workPart.CreateCamSetup("mill_planar");  //相当于目录结构

            var nCGroup1 = (NXOpen.CAM.NCGroup)cAMSetup1.CAMGroupCollection.FindObject("NONE");
            //mill_planar为类型  MCS为其子类型 后期在数据库或本地配置
            //TODO 创建加工坐标系
            var nCGroup2 = (NXOpen.CAM.OrientGeometry)cAMSetup1.CAMGroupCollection.CreateGeometry(nCGroup1, "mill_planar", "MCS",
                NXOpen.CAM.NCGroupCollection.UseDefaultName.False, "MCS_PH");
            //TODO 设置安全平面
            var millOrientGeomBuilder1 = cAMSetup1.CAMGroupCollection.CreateMillOrientGeomBuilder(nCGroup2);
        }
    }
}
