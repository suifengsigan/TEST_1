using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    /// <summary>
    /// EACT电极
    /// </summary>
    public class EactElectrode : Electrode
    {
        public const string BASE_BOT = "EACT_ELEC_BASE_BOTTOM_FACE";
        public const string BASE_TOP = "EACT_ELEC_BASE_TOP_FACE";
        public const string EACT_ELECT_GROUP = "EACT_ELECT_GROUP";

        public EactElectrode()
        {
            ElectrodeType = ElectrodeType.EACT;
        }
        /// <summary>
        /// 基准点
        /// </summary>
        public Snap.NX.Point ElecBasePoint { get; set; }

        public override Snap.Position GetElecBasePos()
        {
            if (ElecBasePoint == null)
            {
                base.GetElecBasePos();
            }
            return ElecBasePoint.Position;
        }
        public static Electrode GetElectrode(Snap.NX.Body body)
        {
            Electrode result = null;
            var elecName = body.Name;
            var faces = body.Faces;
            //顶面
            var topFace = faces.FirstOrDefault(u => u.MatchAttrValue(BASE_BOT, 1));
            //基准面
            var baseFace = faces.FirstOrDefault(u => u.MatchAttrValue( BASE_TOP,1));
            var attrValue = body.GetAttrValue(EACT_ELECT_GROUP);
            //基准点
            var elecBasePoint = Snap.Globals.WorkPart.Points.FirstOrDefault(u => !string.IsNullOrEmpty(attrValue) &&u.MatchAttrValue(EACT_ELECT_GROUP, attrValue));

            if (!string.IsNullOrEmpty(elecName) && topFace != null && baseFace != null)
            {
                var model = new EactElectrode();
                model.BaseFace = baseFace;
                model.TopFace = topFace;
                model.ElecBasePoint = elecBasePoint;
                model.ElecBody = body;
                result = model;
            }
            return result;
        }
    }
}
