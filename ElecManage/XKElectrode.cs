using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    public class XKElectrode : Electrode
    {
        public const string ATTR_NAME_MARK = "ATTR_NAME_MARK";
        public const string BASE_BOT = "BASE_BOT";
        public const string BASE_TOP = "BASE_TOP";
        const string BASE_SIDE = "BASE_SIDE";
        public const string DIM_PT = "DIM_PT";
        const string BASE_CHAMFER = "BASE_CHAMFER";

        /// <summary>
        /// 基准点
        /// </summary>
        public Snap.NX.Point ElecBasePoint { get; set; }

        public XKElectrode() 
        {
            ElectrodeType = ElectrodeType.XK;
        }

        //public override Snap.Position GetElecBasePos()
        //{
        //    return ElecBasePoint.Position;
        //}

        public static Electrode GetElectrode(Snap.NX.Body body)
        {
            Electrode result = null;
            var elecName = body.Name;
            var faces = body.Faces;
            //顶面
            var topFace = faces.FirstOrDefault(u => u.MatchAttrValue(ATTR_NAME_MARK, BASE_BOT));
            //基准面
            var baseFace = faces.FirstOrDefault(u => u.MatchAttrValue(ATTR_NAME_MARK, BASE_TOP));
            //基准台侧面
            var baseSideFaces = faces.Where(u => u.MatchAttrValue(ATTR_NAME_MARK, BASE_SIDE)).ToList();
            //基准点
            var elecBasePoint = Snap.Globals.WorkPart.Points.FirstOrDefault(u => u.MatchAttrValue(ATTR_NAME_MARK, elecName) && u.MatchAttrValue(DIM_PT, DIM_PT));
            //象限面
            var chamferFace = faces.FirstOrDefault(u => u.MatchAttrValue(ATTR_NAME_MARK, BASE_CHAMFER));
            
            if (!string.IsNullOrEmpty(elecName) && topFace != null && baseFace != null && baseSideFaces.Count >= 4 && elecBasePoint != null)
            {
                var model = new XKElectrode();
                model.BaseFace = baseFace;
                model.TopFace = topFace;
                model.BaseSideFaces = baseSideFaces.ToList();
                model.ElecBasePoint = elecBasePoint;
                model.ElecBody = body;
                model.ChamferFace = chamferFace;
                model.ElecHeadFaces = Electrode.GetElecHeadFaces(faces.ToList(), baseFace, out baseSideFaces);
                result = model;
            }
            return result;
        }
    }
}
