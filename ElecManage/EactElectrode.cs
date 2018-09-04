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
                return base.GetElecBasePos();
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
            var baseFaces = faces.Where(u => u.MatchAttrValue(BASE_TOP, 1)).ToList();
            var baseFace = baseFaces.FirstOrDefault();
            if (baseFaces.Count > 1)
            {
                baseFace = baseFaces.OrderByDescending(u => Snap.Compute.Perimeter(u)).FirstOrDefault();
            }
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
                model.AllObject.Add(body);
                if (elecBasePoint != null)
                {
                    model.AllObject.Add(elecBasePoint);
                }
            }
            return result;
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
    }
}
