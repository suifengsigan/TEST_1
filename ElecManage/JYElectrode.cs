using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    /// <summary>
    /// 进玉电极
    /// </summary>
    public class JYElectrode : Electrode
    {
        const string ELEC_BASE_EDM_FACE = "ELEC_BASE_EDM_FACE";
        const string ELEC_BASE_BOTTOM_FACE = "ELEC_BASE_BOTTOM_FACE";
        const string ELEC_SIDE_KZ_FACE = "ELEC_SIDE_KZ_FACE";
        public JYElectrode()
        {
            ElectrodeType = ElectrodeType.JY;
        }
        /// <summary>
        /// 对角线
        /// </summary>
        public Snap.NX.Line DiagonalLine { get; set; }

        public override Snap.NX.Face GetTopFace()
        {
            if (TopFace != null) return TopFace;
            var faceDirection = BaseFace.GetFaceDirection();
            var faces = ElecBody.Faces.ToList();
            var topFace = faces.Where(u => u.IsHasAttr(ELEC_BASE_BOTTOM_FACE)).FirstOrDefault();
            if (topFace == null)
            {
                var topFaces = faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).Where(u =>
                SnapEx.Helper.Equals(-faceDirection, u.GetFaceDirection())
                || SnapEx.Helper.Equals(faceDirection, u.GetFaceDirection())
                ).ToList();
                topFace = topFaces.OrderByDescending(u=>u.GetPlaneProjectArea(faceDirection)).FirstOrDefault();
               
            }
            TopFace = topFace;
            return topFace;
        }

        public override Snap.NX.Face GetChamferFace()
        {
            if (ChamferFace != null) return ChamferFace;
            Snap.NX.Face result = null;
            var results = new List<Snap.NX.Face>();
            if (BaseFace != null && DiagonalLine != null)
            {
                var boxUV = BaseFace.BoxUV;
                var points = new List<Snap.Position>();
                points.Add(BaseFace.Position(boxUV.MinU, boxUV.MinV));
                points.Add(BaseFace.Position(boxUV.MinU, boxUV.MaxV));
                points.Add(BaseFace.Position(boxUV.MaxU, boxUV.MinV));
                points.Add(BaseFace.Position(boxUV.MaxU, boxUV.MaxV));

                points.RemoveAll(u => SnapEx.Helper.Equals(u, DiagonalLine.StartPoint, SnapEx.Helper.Tolerance));
                points.RemoveAll(u => SnapEx.Helper.Equals(u, DiagonalLine.EndPoint, SnapEx.Helper.Tolerance));
                var v1 = Snap.Vector.Unit(points.First() - DiagonalLine.StartPoint);
                var v2 = Snap.Vector.Unit(points.First() - DiagonalLine.EndPoint);
                var v3 = Snap.Vector.Unit(v1 + v2);
                var v4 = v3.Copy(Snap.Geom.Transform.CreateRotation(new Snap.Position(), BaseFace.GetFaceDirection(), 90)).Unitize();

                if (points.Count > 0)
                {
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
                    var cneterPoint =u.GetCenterPointEx();
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

        /// <summary>
        /// 获取基准面
        /// </summary>
        static void GetBaseFace(Snap.NX.Body body, List<Snap.NX.Line> diagonalLines, out Snap.NX.Face face, out Snap.NX.Line line)
        {
            Snap.NX.Face outFace = null;
            Snap.NX.Line outLine = null;
            var faces = body.Faces.ToList();
            faces = faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).ToList();
            var baseFace = faces.Where(u => u.IsHasAttr(ELEC_BASE_EDM_FACE)).FirstOrDefault();
            if (baseFace != null)
            {
                faces = new List<Snap.NX.Face> { baseFace };
            }

            var tempFaces = new List<Snap.NX.Face>();
            if (!Entry.Instance.IsDistinguishSideElec)
            {
                tempFaces = faces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -Snap.Globals.WcsOrientation.AxisZ, SnapEx.Helper.Tolerance)).ToList();
            }
            else
            {
                tempFaces.AddRange(faces.Where(u =>
               SnapEx.Helper.Equals(u.GetFaceDirection(), -Snap.Globals.WcsOrientation.AxisZ, SnapEx.Helper.Tolerance)
               ||SnapEx.Helper.Equals(u.GetFaceDirection(), Snap.Globals.WcsOrientation.AxisZ, SnapEx.Helper.Tolerance)
               || SnapEx.Helper.Equals(u.GetFaceDirection(), -Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance)
                || SnapEx.Helper.Equals(u.GetFaceDirection(), Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance)
                 || SnapEx.Helper.Equals(u.GetFaceDirection(), -Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance)
                  || SnapEx.Helper.Equals(u.GetFaceDirection(), Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance)
                ).ToList());
            }
           
            tempFaces = tempFaces.OrderByDescending(u =>
            {
                var uv = u.BoxUV;
                return Math.Abs(uv.MinU - uv.MaxU) * Math.Abs(uv.MaxV - uv.MinV);
            }).ToList();

            tempFaces.ForEach(u =>
            {
                faces.Remove(u);
            });

            faces = tempFaces;

            foreach (var item in diagonalLines.ToList())
            {
                Snap.NX.Line diagonalLine = item;
                if (Snap.Compute.Distance(diagonalLine, body) >= SnapEx.Helper.Tolerance)
                {
                    diagonalLines.Remove(item);
                }
            }

            foreach (var u in faces)
            {
                var tempDiagonaLines = new List<Snap.NX.Line>();
                foreach (var item in diagonalLines)
                {
                    Snap.NX.Line diagonalLine = item;
                    if (Snap.Compute.Distance(diagonalLine, u) < SnapEx.Helper.Tolerance)
                    {
                        tempDiagonaLines.Add(item);
                    }
                }

                if (tempDiagonaLines.Count > 0)
                {
                    var uBoxUV = u.BoxUV;
                    var centerPoint=u.Position((uBoxUV.MaxU + uBoxUV.MinU) / 2, (uBoxUV.MaxV + uBoxUV.MinV) / 2);
                    foreach (var item in tempDiagonaLines)
                    {
                        Snap.NX.Line diagonalLine = item;
                        if (SnapEx.Helper.Equals(centerPoint, (diagonalLine.StartPoint + diagonalLine.EndPoint) / 2, SnapEx.Helper.Tolerance))
                        {
                            outFace = u;
                            outLine = diagonalLine;
                            face = outFace;
                            line = outLine;
                            return;
                        }
                    }
                }
            }
            face = outFace;
            line = outLine;
        }
        /// <summary>
        /// 获取对角线
        /// </summary>
        static Snap.NX.Line GetDiagonalLine(Snap.NX.Body body, out Snap.NX.Face baseFace)
        {
            Snap.NX.Line result = null;
            baseFace = null;
            var workPart = Snap.Globals.WorkPart;
            var box = body.Box;
            var p2 = new Snap.Position((box.MaxX + box.MinX) / 2, (box.MaxY + box.MinY) / 2, (box.MaxZ + box.MinZ) / 2);
            var lines = new List<Snap.NX.Line>();
            if (!string.IsNullOrEmpty(body.Name))
            {
                lines = workPart.NXOpenPart.Layers.GetAllObjectsOnLayer(body.Layer).ToArray().Where(u => u is NXOpen.Line).Select(u=>Snap.NX.Line.Wrap(u.Tag)).ToList();
                foreach (var line in lines.ToList())
                {
                    if (!Entry.Instance.IsDistinguishSideElec)
                    {
                        var p1 = (line.StartPoint + line.EndPoint) / 2;
                        var tempVec = p1 - p2;
                        var tempOri = Snap.Globals.Wcs.Orientation.AxisZ;
                        if ((
                            SnapEx.Helper.Equals(tempVec, tempOri, SnapEx.Helper.Tolerance)
                            || SnapEx.Helper.Equals(tempVec, -tempOri, SnapEx.Helper.Tolerance)
                            || SnapEx.Helper.Equals(p1, p2, SnapEx.Helper.Tolerance)
                            ) && Snap.Compute.Distance(line, body) < SnapEx.Helper.Tolerance
                            )
                        {

                        }
                        else
                        {
                            lines.Remove(line);
                        }
                    }
                    else
                    {
                        if (Snap.Compute.Distance(line, body) < SnapEx.Helper.Tolerance)
                        {

                        }
                        else
                        {
                            lines.Remove(line);
                        }
                    }
                }
            }
            if (lines.Count > 0)
            {
                GetBaseFace(body, lines, out baseFace, out result);
            }
            return result;
        }
        public static Electrode GetElectrode(Snap.NX.Body body)
        {
            Electrode result = null;
            Snap.NX.Face baseFace = null;
            var diagonalLine = GetDiagonalLine(body, out baseFace);
            if (diagonalLine != null)
            {
                var faceDirection = baseFace.GetFaceDirection();
                var model = new JYElectrode();
                model.DiagonalLine = diagonalLine;
                model.BaseFace = baseFace;
                model.ElecBody = body;
                result = model;

                model.AllObject.Add(body);
                model.AllObject.Add(diagonalLine);
            }
            return result;
        }
    }
}
