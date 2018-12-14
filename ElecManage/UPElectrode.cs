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
        const string BASE_SIDE2 = "2";
        const string BASE_SIDE3 = "3";
        const string BASE_SIDE5 = "5";
        const string BASE_SIDE6 = "6";
        const string BASE_CHAMFER = "1";

        public UPElectrode()
        {
            ElectrodeType = ElectrodeType.UP;
        }

        public override Snap.NX.Face GetChamferFace()
        {
            if (ChamferFace != null) return ChamferFace;
            Snap.NX.Face result = null;
            var results = new List<Snap.NX.Face>();
            if (BaseFace != null)
            {
                var baseFOriention = new Snap.Orientation(BaseFace.GetFaceDirection());
                var v1 = Snap.Vector.Unit(baseFOriention.AxisX);
                var v2 = Snap.Vector.Unit(baseFOriention.AxisY);
                var v3 = Snap.Vector.Unit(v1 + v2);
                var v4 = v3.Copy(Snap.Geom.Transform.CreateRotation(new Snap.Position(), BaseFace.GetFaceDirection(), 90)).Unitize();

                var chamferFaces = ElecBody.Faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane && Snap.Compute.Distance(u, BaseFace) < SnapEx.Helper.Tolerance).ToList();
                chamferFaces.ForEach(u =>
                {
                    var faceDir = u.GetFaceDirection();
                    if (SnapEx.Helper.Equals(v3, faceDir, SnapEx.Helper.Tolerance)
                        || SnapEx.Helper.Equals(-v3, faceDir, SnapEx.Helper.Tolerance)
                        || SnapEx.Helper.Equals(-v4, faceDir, SnapEx.Helper.Tolerance)
                        || SnapEx.Helper.Equals(v4, faceDir, SnapEx.Helper.Tolerance)
                        )
                    {
                        results.Add(u);
                    }
                });
            }

            result = results.FirstOrDefault();

            if (results.Count > 1)
            {
                var baseFaceBoxUV = BaseFace.BoxUV;
                var elecBasePos = BaseFace.Position((baseFaceBoxUV.MinU + baseFaceBoxUV.MaxU) / 2, (baseFaceBoxUV.MinV + baseFaceBoxUV.MaxV) / 2);

                var faceDirection = BaseFace.GetFaceDirection();
                var plane = new Snap.Geom.Surface.Plane(elecBasePos, faceDirection);

                foreach (var u in results)
                {
                    var uv = u.BoxUV;
                    var cneterPoint = u.Position((uv.MaxU + uv.MinU) / 2, (uv.MaxV + uv.MaxV) / 2);
                    var resullt = Snap.Compute.ClosestPoints(cneterPoint, plane);
                    var dir = Snap.Vector.Unit(resullt.Point1 - resullt.Point2);
                    if (SnapEx.Helper.Equals(dir, -faceDirection) && Snap.Compute.Distance(BaseFace, u) < SnapEx.Helper.Tolerance)
                    {
                        result = u;
                        break;
                    }
                }
            }

            ChamferFace = result;

            return result;
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
            var baseSideFaces = faces.Where(u => 
            u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_SIDE2)
            || u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_SIDE3)
            || u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_SIDE5)
            || u.MatchAttrValue(UP_PART_MOLD_FACE_TYPE, BASE_SIDE6)
            ).ToList();
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
