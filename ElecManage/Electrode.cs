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

        public static void SetEactElectrode(Snap.NX.Body body,ref Snap.NX.Face topFace,ref Snap.NX.Face baseFace,ref Snap.NX.Point basePoint)
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
                var topDir = new Snap.Vector(0, 0, 1);
                //顶面
                topFace = faces.Where(u => u.IsHasAttr(EactElectrode.BASE_BOT)|| u.MatchAttrValue(XKElectrode.ATTR_NAME_MARK, XKElectrode.BASE_BOT)).FirstOrDefault();
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

                if (basePoint == null&&baseFace!=null)
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
                body.SetStringAttribute(EactElectrode.EACT_ELECT_GROUP,guid);
                basePoint.SetStringAttribute(EactElectrode.EACT_ELECT_GROUP, guid);
            }
        }
        /// <summary>
        /// 电极类型
        /// </summary>
        public ElectrodeType ElectrodeType=ElectrodeType.UNKOWN;
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

        public virtual Snap.NX.Face GetTopFace() 
        {
            return TopFace;
        }

        public virtual Snap.NX.Face GetChamferFace() 
        {
            return ChamferFace;
        }

        /// <summary>
        /// 获取象限类型
        /// </summary>
        public QuadrantType GetQuadrantType() 
        {
            var result = QuadrantType.First;
            var chamferFace = GetChamferFace();
            if (chamferFace != null) 
            {
                var dir = SnapEx.Create.MapAcsToWcs(chamferFace.GetFaceDirection(),new Snap.Orientation(-BaseFace.GetFaceDirection()));
                result = SnapEx.Helper.GetQuadrantType(dir);
            }
            return result;
        }

        public ElectrodeInfo GetElectrodeInfo() 
        {
            ElectrodeInfo result = new ElectrodeInfo(ElecBody);
            if (ElectrodeType == ElectrodeType.XK)
            {
                result= new XKElectrodeInfo(ElecBody);
            }
            else if (ElectrodeType == ElectrodeType.EACT)
            {
                result = new EactElectrodeInfo(ElecBody);
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
                result = EactElectrode.GetElectrode(body);
            }
            if (result == null)
            {
                result = JYElectrode.GetElectrode(body);
            }
            return result;
        }

        /// <summary>
        /// 初始化所有的面
        /// </summary>
        public virtual void InitAllFace() 
        {
            GetTopFace();
            var allSideFaces = new List<Snap.NX.Face>();
            var faces = ElecBody.Faces.ToList();
            ElecHeadFaces = Electrode.GetElecHeadFaces(faces, BaseFace, out allSideFaces);
            BaseSideFaces = Electrode.GetBaseSideFaces(BaseFace, allSideFaces);
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
                        var uv = u.Box;
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
            var orientation = new Snap.Orientation(faceDirection);

            result.AddRange(allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -orientation.AxisY)));
            result.AddRange(allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), orientation.AxisX)));
            result.AddRange(allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), orientation.AxisY)));
            result.AddRange(allSideFaces.Where(u => SnapEx.Helper.Equals(u.GetFaceDirection(), -orientation.AxisX)));
            return result;
        }

    }
}