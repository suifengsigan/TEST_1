using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    public abstract class Electrode : IElectrode
    {
        public Electrode()
        {
            BaseSideFaces = new List<Snap.NX.Face>();
            ElecHeadFaces = new List<Snap.NX.Face>();
        }

        public static void GetEactElectrode(Snap.NX.Body body, ref Snap.NX.Face topFace, ref Snap.NX.Face baseFace, ref Snap.NX.Point basePoint, ref Snap.Position pos)
        {
            var faces = body.Faces.ToList();
            var topDir = new Snap.Vector(0, 0, 1);
            //顶面
            topFace = faces.Where(u => u.IsHasAttr(EactElectrode.BASE_BOT) || u.MatchAttrValue(XKElectrode.ATTR_NAME_MARK, XKElectrode.BASE_BOT)).FirstOrDefault();
            if (topFace == null)
            {
                var topFaces = faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).Where(u => SnapEx.Helper.Equals(topDir, u.GetFaceDirection()));
                topFace = topFaces.FirstOrDefault();
                if (topFaces.Count() > 1)
                {
                    topFace = topFaces.OrderByDescending(u => {
                        var uv = u.BoxUV;
                        return Math.Abs(uv.MinU - uv.MaxU) * Math.Abs(uv.MaxV - uv.MinV);
                    }).FirstOrDefault();
                }
            }
            //基准面
            baseFace = faces.Where(u => u.IsHasAttr(EactElectrode.BASE_TOP) || u.MatchAttrValue(XKElectrode.ATTR_NAME_MARK, XKElectrode.BASE_TOP)).FirstOrDefault();
            if (baseFace == null)
            {
                var baseFaces = faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).Where(u => SnapEx.Helper.Equals(-topDir, u.GetFaceDirection()));
                if (baseFaces.Count() == 0)
                {
                    baseFace = topFace;
                }
                else
                {
                    baseFace = baseFaces.OrderByDescending(u => {
                        var uv = u.BoxUV;
                        return Math.Abs(uv.MinU - uv.MaxU) * Math.Abs(uv.MaxV - uv.MinV);
                    }).FirstOrDefault();
                }
            }

            if (topFace == null)
            {
                topFace = baseFace;
            }

            //基准点
            var basePoints = Snap.Globals.WorkPart.Points.Where(u => u.Layer == body.Layer).ToList().OrderBy(u => Snap.Compute.Distance(u.Position, body)).ToList();
            basePoint = basePoints.FirstOrDefault();
            var tempBasePoint = basePoints.FirstOrDefault(u => u.MatchAttrValue(XKElectrode.ATTR_NAME_MARK, body.Name) && u.MatchAttrValue(XKElectrode.DIM_PT, XKElectrode.DIM_PT));
            if (tempBasePoint != null)
            {
                basePoint = tempBasePoint;
            }

            if (basePoint == null && baseFace != null)
            {
                pos = baseFace.GetCenterPoint();
            }
            else if (basePoint != null)
            {
                pos = basePoint.Position;
            }
        }

        public static void SetEactElectrode(Snap.NX.Body body, ref Snap.NX.Face topFace, ref Snap.NX.Face baseFace, ref Snap.NX.Point basePoint)
        {
            var faces = body.Faces.ToList();
            //清空相关属性
            if (body.IsHasAttr(EactElectrode.EACT_ELECT_GROUP))
            {
                body.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, EactElectrode.EACT_ELECT_GROUP);
            }
            faces.ToList().ForEach(u => {
                if (u.IsHasAttr(EactElectrode.EACT_ELECT_GROUP))
                {
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, EactElectrode.EACT_ELECT_GROUP);
                }

                if (u.IsHasAttr(EactElectrode.BASE_BOT))
                {
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, EactElectrode.BASE_BOT);
                }

                if (u.IsHasAttr(EactElectrode.BASE_TOP))
                {
                    u.DeleteAttributes(Snap.NX.NXObject.AttributeType.String, EactElectrode.BASE_TOP);
                }
            });

            if (topFace != null && baseFace != null && basePoint != null)//手动
            {

            }
            else//自动
            {
                Snap.Position basePos = new Snap.Position();
                GetEactElectrode(body, ref topFace, ref baseFace, ref basePoint, ref basePos);
                if (basePoint == null && baseFace != null)
                {
                    basePoint = Snap.Create.Point(baseFace.GetCenterPoint());
                    basePoint.Layer = body.Layer;
                    basePoint.Color = System.Drawing.Color.Green;
                }
            }

            //赋属性
            if (topFace != null)
            {
                topFace.SetStringAttribute(EactElectrode.BASE_BOT, "1");
            }
            if (baseFace != null)
            {
                baseFace.SetStringAttribute(EactElectrode.BASE_TOP, "1");
            }
            if (basePoint != null)
            {
                var guid = Guid.NewGuid().ToString();
                body.SetStringAttribute(EactElectrode.EACT_ELECT_GROUP, guid);
                basePoint.SetStringAttribute(EactElectrode.EACT_ELECT_GROUP, guid);
            }

            if (topFace != null&& baseFace != null&& basePoint != null)
            {
                body.SetStringAttribute(ElectrodeInfo.EACT_SPECIALSHAPED, "1");
            }
        }
        /// <summary>
        /// 电极类型
        /// </summary>
        public ElectrodeType ElectrodeType = ElectrodeType.UNKOWN;
        /// <summary>
        /// 电极体
        /// </summary>
        public Snap.NX.Body ElecBody { get; set; }
        /// <summary>
        /// 顶面
        /// </summary>
        public Snap.NX.Face TopFace { get; set; }
        /// <summary>
        /// 基准面
        /// </summary>
        public Snap.NX.Face BaseFace { get; set; }
        /// <summary>
        /// 基准台侧面
        /// </summary>
        public List<Snap.NX.Face> BaseSideFaces { get; set; }
        /// <summary>
        /// 电极头部面
        /// </summary>
        public List<Snap.NX.Face> ElecHeadFaces { get; set; }
        /// <summary>
        /// 象限面
        /// </summary>
        public Snap.NX.Face ChamferFace { get; set; }
        /// <summary>
        /// 电极中所有的对象
        /// </summary>

        public List<Snap.NX.NXObject> AllObject = new List<Snap.NX.NXObject>();

        public virtual Snap.NX.Face GetTopFace()
        {
            return TopFace;
        }

        public virtual Snap.NX.Face GetChamferFace()
        {
            return ChamferFace;
        }

        /// <summary>
        /// 获取检测类型
        /// </summary>

        public QuadrantType GetCMMQuadrantType(QuadrantType defaultQ = QuadrantType.First)
        {
            var result = defaultQ;
            var chamferFace = GetChamferFace();
            if (chamferFace != null)
            {
                var dir = chamferFace.GetFaceDirection();
                result = SnapEx.Helper.GetQuadrantType(dir);
            }
            return result;
        }

        public static Snap.Geom.Transform GetElecTransWcsToAcs(Snap.Vector baseDir)
        {
            return GetElecTransWcsToAcs(baseDir, Snap.Globals.WcsOrientation);
        }

        public static Snap.Geom.Transform GetElecTransWcsToAcs(Snap.Vector baseDir,Snap.Orientation wcs)
        {
            var wcsOrientation = GetStandardOrientation(wcs);
            var acsOrientation = Snap.Orientation.Identity;
            var transR = Snap.Geom.Transform.CreateRotation(acsOrientation, wcsOrientation);
            var baseFaceOrientation = new Snap.Orientation(-baseDir.Copy(transR));
            baseFaceOrientation=GetSidelongOrientation(baseFaceOrientation);
            transR = Snap.Geom.Transform.Composition(transR, Snap.Geom.Transform.CreateRotation(acsOrientation, baseFaceOrientation));
            return transR;
        }

        /// <summary>
        /// 获取侧放矩阵
        /// </summary>
        public static Snap.Orientation GetSidelongOrientation(Snap.Orientation baseDirOrientation)
        {
            var or = GetStandardOrientation(baseDirOrientation);
            var identity = Snap.Orientation.Identity;
            Snap.Vector x = or.AxisX;
            Snap.Vector y = or.AxisY;
            if (SnapEx.Helper.Equals(or.AxisZ, -identity.AxisY))
            {
                x = identity.AxisX;
                y = identity.AxisZ;
            }
            else if (SnapEx.Helper.Equals(or.AxisZ, identity.AxisY))
            {
                x = -identity.AxisX;
                y = identity.AxisZ;
            }
            else if (SnapEx.Helper.Equals(or.AxisZ, -identity.AxisX))
            {
                x = -identity.AxisY;
                y = identity.AxisZ;
            }
            else if (SnapEx.Helper.Equals(or.AxisZ, identity.AxisX))
            {
                x = identity.AxisY;
                y = identity.AxisZ;
            }
            else if (SnapEx.Helper.Equals(or.AxisZ, -identity.AxisZ))
            {
                x = identity.AxisX;
                y = -identity.AxisY;
            }
            else
            {
                return or;
            }
            return new Snap.Orientation(x, y);
        }

        public static Snap.Orientation GetStandardOrientation(Snap.Orientation ori)
        {
            var result = ori;
            if (!IsUseOrientation(ori))
            {
                var vectors = new List<Snap.Vector>();
                var identiry = Snap.Orientation.Identity;
                vectors.Add(identiry.AxisX);
                vectors.Add(identiry.AxisY);
                vectors.Add(identiry.AxisZ);
                vectors.Add(-identiry.AxisX);
                vectors.Add(-identiry.AxisY);
                vectors.Add(-identiry.AxisZ);
                var axisXs = vectors.Where(u => SnapEx.Helper.Equals(u, ori.AxisX)).ToList();
                var axisYs = vectors.Where(u => SnapEx.Helper.Equals(u, ori.AxisY)).ToList();
                var axisZs = vectors.Where(u => SnapEx.Helper.Equals(u, ori.AxisZ)).ToList();
                if (axisXs.Count > 0 && axisYs.Count > 0 && axisZs.Count > 0)
                {
                    result = new Snap.Orientation(axisXs.First(), axisYs.First(), axisZs.First());
                }
            }

            return result;
        }

        /// <summary>
        /// 获取加工方向(YF_YJQ)
        /// </summary>
        public static string GetCNC_DIRECTION(Snap.Vector topFaceDir)
        {
            var temptopFaceDir = topFaceDir;
            var tempAixValue = "Z-";
            if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisZ, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "Z+";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "X-";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "X+";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "Y-";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "Y+";
            }

            return tempAixValue;
        }

        /// <summary>
        /// 获取加工方向
        /// </summary>
        public static string GetDIRECTION(Snap.Vector topFaceDir)
        {
            var temptopFaceDir = topFaceDir;
            var tempAixValue = "Z+";
            if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisZ, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "Z-";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "X+";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisX, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "X-";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "Y+";
            }
            if (SnapEx.Helper.Equals(temptopFaceDir, -Snap.Globals.WcsOrientation.AxisY, SnapEx.Helper.Tolerance))
            {
                tempAixValue = "Y-";
            }

            return tempAixValue;
        }

        public static bool IsUseOrientation(Snap.Orientation ori)
        {
            var result = true;
            List<double> list = new List<double>();
            list.AddRange(ori.AxisX.Array);
            list.AddRange(ori.AxisY.Array);
            list.AddRange(ori.AxisZ.Array);
            foreach (var item in list)
            {
                if (!(System.Math.Round(item) == item))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取象限类型
        /// </summary>
        public QuadrantType GetQuadrantType(QuadrantType defaultQ=QuadrantType.First) 
        {
            var result = defaultQ;
            var chamferFace = GetChamferFace();
            if (chamferFace != null&&BaseFace!=null) 
            {
                var dir = chamferFace.GetFaceDirection().Copy(GetElecTransWcsToAcs(BaseFace.GetFaceDirection()));
                result = SnapEx.Helper.GetQuadrantType(dir);
            }
            return result;
        }

        /// <summary>
        /// 获取象限类型
        /// </summary>
        public QuadrantType GetQuadrantType(Snap.Orientation wcs,QuadrantType defaultQ = QuadrantType.First)
        {
            var result = defaultQ;
            var chamferFace = GetChamferFace();
            if (chamferFace != null && BaseFace != null)
            {
                var dir = chamferFace.GetFaceDirection().Copy(GetElecTransWcsToAcs(BaseFace.GetFaceDirection(),wcs));
                result = SnapEx.Helper.GetQuadrantType(dir);
            }
            return result;
        }

        public ElectrodeInfo GetElectrodeInfo() 
        {
            ElectrodeInfo result = new ElectrodeInfo(ElecBody);
            if (ElectrodeType == ElectrodeType.XK)
            {
                result = new XKElectrodeInfo(ElecBody);
            }
            else if (ElectrodeType == ElectrodeType.EACT)
            {
                result = new EactElectrodeInfo(ElecBody);
            }
            else if (ElectrodeType == ElectrodeType.UP)
            {
                result = new UPElectrodeInfo(ElecBody);
            }
            result.Electrode = this;
            return result;
        }

        public virtual Snap.Position GetElecBasePos() 
        {
            var uv=BaseFace.BoxUV;
            return BaseFace.Position((uv.MinU + uv.MaxU) / 2, (uv.MinV + uv.MaxV) / 2);
        }
        public static Electrode GetElectrode(Snap.NX.Body body) 
        {
            Electrode result = null;
            result = XKElectrode.GetElectrode(body);
            if (result == null)
            {
                result = UPElectrode.GetElectrode(body);
            }
            if (result == null)
            {
                result = JYElectrode.GetElectrode(body);
            }
            if (result == null) 
            {
                result = EactElectrode.GetElectrode(body);
            }
            return result;
        }

        private bool isInitAllFace = false;
        /// <summary>
        /// 初始化所有的面
        /// </summary>
        public virtual void InitAllFace() 
        {
            if (!isInitAllFace)
            {
                GetTopFace();
                var allSideFaces = new List<Snap.NX.Face>();
                var faces = ElecBody.Faces.ToList();
                ElecHeadFaces = Electrode.GetElecHeadFaces(faces, BaseFace, out allSideFaces);
                BaseSideFaces = Electrode.GetBaseSideFaces(BaseFace, allSideFaces);
                isInitAllFace = true;
            } 
        }

        /// <summary>
        /// 获取所有的电极头部面
        /// </summary>
        protected static List<Snap.NX.Face> GetElecHeadFaces(List<Snap.NX.Face> faces, Snap.NX.Face baseFace,out List<Snap.NX.Face> sideFaces) 
        {
            var headFaces = new List<Snap.NX.Face>();
            var tempSideFaces = new List<Snap.NX.Face>();
            if (baseFace != null) 
            {
                //头部面
                var allFace = faces.ToList();
                var baseFaceBoxUV = baseFace.BoxUV;
                var elecBasePos = baseFace.Position((baseFaceBoxUV.MinU + baseFaceBoxUV.MaxU) / 2, (baseFaceBoxUV.MinV + baseFaceBoxUV.MaxV) / 2);
                allFace.RemoveAll(u => u.NXOpenTag == baseFace.NXOpenTag);
                var faceDirection = baseFace.GetFaceDirection();
                var plane = new Snap.Geom.Surface.Plane(elecBasePos, faceDirection);

                allFace.ToList().ForEach(u =>
                {
                    try
                    {
                        var uv = u.BoxEx();
                        var cneterPoint = new Snap.Position((uv.MaxX+uv.MinX)/2, (uv.MaxY + uv.MinY) / 2, (uv.MaxZ + uv.MinZ) / 2);
                        var resullt = Snap.Compute.ClosestPoints(cneterPoint, plane);
                        var dir = Snap.Vector.Unit(resullt.Point1 - resullt.Point2);
                        if (SnapEx.Helper.Equals(dir, faceDirection))
                        {
                            headFaces.Add(u);
                        }
                        else if (u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane && SnapEx.Helper.Equals(dir, -faceDirection))
                        {
                            tempSideFaces.Add(u);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
            }
            sideFaces = tempSideFaces;
            return headFaces;
        }

        /// <summary>
        /// 获取所有的侧面
        /// </summary>
        protected static List<Snap.NX.Face> GetBaseSideFaces(Snap.NX.Face baseFace,List<Snap.NX.Face> allSideFaces)
        {
            var result = new List<Snap.NX.Face>();
            var faceDirection = baseFace.GetFaceDirection();
            var baseFaceCenterPoint = baseFace.GetCenterPoint();
            var orientation = new Snap.Orientation(faceDirection);
            var firstFace = allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -orientation.AxisY));
            var twoFace = allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), orientation.AxisX));
            var threeFace = allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), orientation.AxisY));
            var fourFace = allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -orientation.AxisX));

            Action<List<Snap.NX.Face>> action = itemFace => {
                if (itemFace.Count() > 1)
                {
                    var fFace = itemFace
                    .Where(u => Snap.Compute.Distance(u, baseFace) < SnapEx.Helper.Tolerance)
                    .OrderByDescending(u => Snap.Compute.Distance(baseFaceCenterPoint, ((Snap.NX.Face.Plane)u).Geometry))
                    .ThenByDescending(u=>u.GetPlaneProjectArea())
                    .FirstOrDefault();
                    if (fFace != null)
                    {
                        result.Add(fFace);
                    }
                }
                else
                {
                    result.AddRange(itemFace);
                }
            };

            action(firstFace.ToList());
            action(twoFace.ToList());
            action(threeFace.ToList());
            action(fourFace.ToList());
            return result;
        }

    }
}