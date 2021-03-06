﻿using System;
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

        protected override Snap.NX.Face GetTopFace()
        {
            var faceDirection = BaseFace.GetFaceDirection();
            var faces = ElecBody.Faces.ToList();
            var topFace = faces.Where(u => u.IsHasAttr(ELEC_BASE_BOTTOM_FACE)).FirstOrDefault();
            if (topFace == null)
            {
                var topFaces = faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).Where(u => SnapEx.Helper.Equals(-faceDirection, u.GetFaceDirection())).ToList();
                topFace=topFaces.FirstOrDefault();
                if (topFaces.Count > 1) 
                {
                    var basePlane = BaseFace as Snap.NX.Face.Plane;
                    var baseOrigin = basePlane.Geometry.Origin;
                    var baseFaceBox = BaseFace.Box;
                    var baseCenterPoint = new Snap.Position((baseFaceBox.MaxX + baseFaceBox.MinX) / 2, (baseFaceBox.MaxY + baseFaceBox.MinY) / 2, (baseFaceBox.MaxZ + baseFaceBox.MinZ) / 2);
                    topFaces.ForEach(u => {
                        var uBox=u.Box;
                        var uCenterPoint = new Snap.Position((uBox.MaxX + uBox.MinX) / 2, (uBox.MaxY + uBox.MinY) / 2, (uBox.MaxZ + uBox.MinZ) / 2);
                        if (SnapEx.Helper.Equals(u.GetFaceDirection(), Snap.Vector.Unit(uCenterPoint - baseCenterPoint),SnapEx.Helper.Tolerance)) 
                        {
                            topFace = u;
                            return;
                        }
                    });
                }
            }
            TopFace = topFace;
            return topFace;
        }

        public override Snap.NX.Face GetChamferFace() 
        {
            Snap.NX.Face result = null;
            var results = new List<Snap.NX.Face>();
            if (BaseFace != null && DiagonalLine != null) 
            {
                var boxUV = BaseFace.BoxUV;
                var points=new List<Snap.Position>();
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

                if(points.Count>0)
                {
                    ElecBody.Faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).ToList().ForEach(u =>
                    {
                        var faceDir=u.GetFaceDirection();
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

                results.ForEach(u => {
                    var uv = u.BoxUV;
                    var cneterPoint = u.Position((uv.MaxU + uv.MinU) / 2, (uv.MaxV + uv.MaxV) / 2);
                    var resullt = Snap.Compute.ClosestPoints(cneterPoint, plane);
                    var dir = Snap.Vector.Unit(resullt.Point1 - resullt.Point2);
                    if (SnapEx.Helper.Equals(dir, -faceDirection))
                    {
                        result = u;
                        return;
                    }
                });
            }

            ChamferFace = result;

            return result;
        }

        /// <summary>
        /// 获取基准面
        /// </summary>
        static void GetBaseFace(Snap.NX.Body body, List<Snap.NX.Line> diagonalLines,out Snap.NX.Face face,out Snap.NX.Line line)
        {
            Snap.NX.Face outFace = null;
            Snap.NX.Line outLine = null;
            var faces=body.Faces.ToList();
            faces = faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).ToList();
            var baseFace = faces.Where(u => u.IsHasAttr(ELEC_BASE_EDM_FACE)).FirstOrDefault();
            if (baseFace != null) 
            {
                faces = new List<Snap.NX.Face>{ baseFace };
            }
            var tempFaces=faces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -Snap.Globals.WcsOrientation.AxisZ,SnapEx.Helper.Tolerance)).ToList();
            tempFaces = tempFaces.OrderByDescending(u => {
                var uv = u.BoxUV;
                return Math.Abs(uv.MinU - uv.MaxU) * Math.Abs(uv.MaxV - uv.MinV);
            }).ToList();

            tempFaces.ForEach(u =>
            {
                faces.Remove(u);
            });

            faces.InsertRange(0, tempFaces);

            foreach (var u in faces) 
            {
                var corners = u.Box.Corners.Distinct().ToList();
                foreach (var item in diagonalLines)
                {
                    Snap.NX.Line diagonalLine = item;
                    if (Snap.Compute.Distance(diagonalLine, u) < SnapEx.Helper.Tolerance)
                    {
                        var startPos = corners.Where(m => SnapEx.Helper.Equals(m, diagonalLine.StartPoint, SnapEx.Helper.Tolerance));
                        var endPos = corners.Where(m => SnapEx.Helper.Equals(m, diagonalLine.EndPoint, SnapEx.Helper.Tolerance));
                        if (startPos.Count() > 0 && endPos.Count() > 0)
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
        static Snap.NX.Line GetDiagonalLine(Snap.NX.Body body,out Snap.NX.Face baseFace)
        {
            Snap.NX.Line result = null;
            var workPart = Snap.Globals.WorkPart;
            var box = body.Box;
            var p2 = new Snap.Position((box.MaxX + box.MinX) / 2, (box.MaxY + box.MinY) / 2);
            var lines = new List<NXOpen.NXObject>();
            if (!string.IsNullOrEmpty(body.Name))
            {
                lines = workPart.NXOpenPart.Layers.GetAllObjectsOnLayer(body.Layer).Where(l => l is NXOpen.Line).ToList();
                lines.ToList().ForEach(u =>
                {
                    Snap.NX.Line line = u as NXOpen.Line;
                    bool isRemove = true;
                    if (Snap.Compute.Distance(line, body) < SnapEx.Helper.Tolerance) 
                    {
                        var p1 = (line.StartPoint + line.EndPoint) / 2;
                        p2.Z = p1.Z;
                        if (SnapEx.Helper.Equals(p1, p2, SnapEx.Helper.Tolerance))
                        {
                            isRemove = false;
                        }
                    }

                    if (isRemove) 
                    {
                        lines.Remove(u);
                    }
                   
                });
            }
            GetBaseFace(body, Enumerable.Select(lines, u => Snap.NX.Line.Wrap(u.Tag)).ToList(), out baseFace, out result);
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
            }
            return result;
        }
    }
}
