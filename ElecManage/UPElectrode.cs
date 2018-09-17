using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    public class UPElectrode : Electrode
    {
        public const string UP_PART_MOLD_FACE_TYPE = "UP_PART_MOLD_FACE_TYPE";
        public const string BASE_BOT = "4";
        public const string BASE_TOP = "7";
        const string BASE_SIDE = "2356";
        const string BASE_CHAMFER = "1";

        public UPElectrode()
        {
            ElectrodeType = ElectrodeType.XK;
        }

        public static Electrode GetElectrode(Snap.NX.Body body)
        {
            Electrode result = null;
            var elecName = body.Name;
            var faces = body.Faces;
            //顶面
            var topFace = faces.FirstOrDefault(u => u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_BOT));
            //基准面
            var baseFace = faces.FirstOrDefault(u => u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_TOP));
            //基准台侧面
            var baseSideFaces = faces.Where(u => BASE_SIDE.Contains(u.GetAttrValue(UP_PART_MOLD_FACE_TYPE))).ToList();
            //象限面
            var chamferFace = faces.FirstOrDefault(u => u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_CHAMFER));

            if (!string.IsNullOrEmpty(elecName) && topFace != null && baseFace != null && baseSideFaces.Count >= 4)
            {
                var model = new UPElectrode();
                model.BaseFace = baseFace;
                model.TopFace = topFace;
                model.BaseSideFaces = baseSideFaces.ToList();
                model.ElecBody = body;
                model.ChamferFace = chamferFace;
                //model.ElecHeadFaces = Electrode.GetElecHeadFaces(faces.ToList(), baseFace, out baseSideFaces);
                result = model;
                model.AllObject.Add(body);
            }
            return result;
        }
    }
}
