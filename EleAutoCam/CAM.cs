using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snap;
using NXOpen;

namespace SnapEx
{
    public class CAMEx
    {
        static NXOpen.UF.UFSession _ufSession = NXOpen.UF.UFSession.GetUFSession();

        /// <summary>
        /// 设置面边界
        /// </summary>
        public static void SetBoundaryByFace(NXOpen.UF.CamGeomType camGeomType, NXOpen.Tag operTag, NXOpen.Tag faceTag, NXOpen.UF.CamMaterialSide materialSide) 
        {
            var boundary_data = new NXOpen.UF.UFCambnd.BoundaryData();
            boundary_data.boundary_type = NXOpen.UF.CamBoundaryType.CamBoundaryTypeClosed;
            boundary_data.plane_type = 1;
            boundary_data.material_side = materialSide;
            boundary_data.ignore_holes = 0;
            boundary_data.ignore_islands = 0;
            boundary_data.ignore_chamfers = 0;
            boundary_data.app_data = new NXOpen.UF.UFCambnd.AppData[] { };
            _ufSession.Cambnd.AppendBndFromFace(operTag, camGeomType, faceTag, ref boundary_data);

        }

        /// <summary>
        /// 设置区域
        /// </summary>
        public static void SetMillArea(NXOpen.UF.CamGeomType camGeomType,NXOpen.Tag operTag,List<NXOpen.Tag> cutAreaGeometryTags) 
        {
            var appDatas = new List<NXOpen.UF.UFCamgeom.AppData>();
            cutAreaGeometryTags.ForEach(u =>
            {
                var appData = new NXOpen.UF.UFCamgeom.AppData();
                appData.has_stock = 0;
                appData.has_cut_stock = 0;
                appData.has_tolerances = 0;
                appData.has_feedrate = 0;
                appData.has_offset = 0;
                appData.has_avoidance_type = 0;
                appData.offset = 0.1;
                appDatas.Add(appData);
            });
            _ufSession.Camgeom.DeleteGeometry(operTag, camGeomType);
            _ufSession.Camgeom.AppendItems(operTag, camGeomType, cutAreaGeometryTags.Count, cutAreaGeometryTags.ToArray(), appDatas.ToArray());
        }

        /// <summary>
        /// 路径生成
        /// </summary>
        public static void PathGenerate(List<NXOpen.Tag> operTags)
        {
            bool generated;
            operTags.ForEach(u =>
            {
                _ufSession.Param.Generate(u, out generated);
            });
        }

        /// <summary>
        /// 显示刀具路径
        /// </summary>
        public static void ShowCutterPath(NXOpen.Tag operTag)
        {
            _ufSession.Param.ReplayPath(operTag);
        }
    }
}
