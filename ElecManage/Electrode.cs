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
            if (ElectrodeType == ElectrodeType.XK) 
            {
                return new XKElectrodeInfo(ElecBody);
            }
            return new ElectrodeInfo(ElecBody);
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
                    var uv = u.BoxUV;
                    var cneterPoint = u.Position((uv.MaxU + uv.MinU) / 2, (uv.MaxV + uv.MinV) / 2);
                    var resullt = Snap.Compute.ClosestPoints(cneterPoint, plane);
                    var dir = Snap.Vector.Unit(resullt.Point1 - resullt.Point2);
                    if (SnapEx.Helper.Equals(dir, faceDirection))
                    {
                        headFaces.Add(u);
                    }
                    else if(u.ObjectSubType==Snap.NX.ObjectTypes.SubType.FacePlane&&SnapEx.Helper.Equals(dir,-faceDirection)) 
                    {
                        tempSideFaces.Add(u);
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