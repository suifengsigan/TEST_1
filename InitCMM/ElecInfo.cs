using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SnapEx;

namespace CMMTool
{
    public class JYElecHelper
    {
        public const double _tolerance = 0.0001;
        public static bool IsElec(Snap.NX.Body body)
        {
            bool result = false;
            result = body.GetAttributeInfo().Where(u => u.Title == JYElecConst.ELEC_FINISH_NUMBER || u.Title == JYElecConst.ELEC_MIDDLE_NUMBER || u.Title == JYElecConst.ELEC_ROUGH_NUMBER).Count() > 0;
            return result;
        }

        /// <summary>
        /// 获取基准面
        /// </summary>
        public static Snap.NX.Face GetBaseFace(Snap.Position startPoint,Snap.Position endPoint,Snap.NX.Body body) 
        {
            Snap.Position basePoint = (startPoint + endPoint) / 2;

            Snap.NX.Face baseFace = null;

            var faces = body.Faces.ToList();
            faces.ForEach(u =>
            {
                var box = u.BoxUV;
                var p1 = u.Position((box.MinU + box.MaxU) / 2, (box.MaxV + box.MinV) / 2);
                if (SnapEx.Helper.Equals(p1, basePoint, _tolerance))
                {
                    baseFace = u;
                    return;
                }
            });

            return baseFace;
        }

        /// <summary>
        /// 获取基准角
        /// </summary>
        public static Snap.NX.Face GetBaseCornerFace(Snap.Position startPoint,Snap.Position endPoint,Snap.NX.Body body) 
        {
            Snap.NX.Face result = null;
            Snap.Position basePoint=(startPoint+endPoint)/2;

            Snap.NX.Face baseFace = GetBaseFace(startPoint, endPoint, body);

            var faces = body.Faces.ToList();
            if (baseFace != null) 
            {
                faces.Remove(baseFace);

                var baseFaceDir = new Snap.Orientation(baseFace.GetFaceDirection());

                var tempFaces = new List<Snap.NX.Face>();

                faces.Where(u => u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane).ToList().ForEach(u =>
                {
                    var director = u.GetFaceDirection();
                    var angle = Snap.Vector.Angle(baseFaceDir.AxisX, director);
                    if (Math.Abs(angle - 45) < _tolerance
                        || Math.Abs(angle - 135) < _tolerance
                        )
                    {
                        tempFaces.Add(u);
                    }
                });

                if (tempFaces.Count > 1)
                {
                    tempFaces.RemoveAll(u => u.Box.MaxZ <= basePoint.Z);
                }

                if (tempFaces.Count > 0) 
                {
                    result= tempFaces.FirstOrDefault();
                }
            }

            return result;
        }
        

        public static double GetAttrValue(Snap.NX.Body body,string title)
        {
            double d;
            if (body.GetAttributeInfo().Where(u => u.Title == title).Count() > 0)
            {
                var attr = body.GetAttributeInfo().FirstOrDefault(u => u.Title == title);
                switch (attr.Type)
                {
                    case Snap.NX.NXObject.AttributeType.Integer:
                        {
                            d = body.GetIntegerAttribute(title);
                            break;
                        }
                    case Snap.NX.NXObject.AttributeType.Real:
                        {
                            d = body.GetRealAttribute(title);
                            break; 
                        }
                    default:
                        {
                            d = 0;
                            break;
                        }
                }
            }
            else 
            {
                d = 0;
            }
            return d;
        }
    }
    public struct JYElecConst
    {
        public const string ELEC_FINISH_NUMBER = "ELEC_FINISH_NUMBER";
        public const string ELEC_MIDDLE_NUMBER = "ELEC_MIDDLE_NUMBER";
        public const string ELEC_ROUGH_NUMBER = "ELEC_ROUGH_NUMBER";

        public const string ELEC_FINISH_SPACE = "ELEC_FINISH_SPACE";
        public const string ELEC_MIDDLE_SPACE = "ELEC_MIDDLE_SPACE";
        public const string ELEC_ROUGH_SPACE = "ELEC_ROUGH_SPACE";

        public const string MODEL_NUMBER = "MODEL_NUMBER";  //模号
        public const string MR_NUMBER = "MR_NUMBER";//件号

        public const string ELEC_MAT_NAME = "ELEC_MAT_NAME"; //材质
        public const string ELEC_F_MAT_NAME = "ELEC_F_MAT_NAME";
        public const string ELEC_M_MAT_NAME = "ELEC_M_MAT_NAME";
        public const string ELEC_R_MAT_NAME = "ELEC_R_MAT_NAME";

        public const string CLAMP_GENERAL_TYPE = "CLAMP_GENERAL_TYPE";//夹具类型

        public const string MR_MATERAL = "MR_MATERAL";//钢件材质

        public const string ELEC_MACH_TYPE = "ELEC_MACH_TYPE"; //电极类型

        public const string F_ELEC_SMOOTH = "F_ELEC_SMOOTH";//光洁度
        public const string M_ELEC_SMOOTH = "M_ELEC_SMOOTH";
        public const string R_ELEC_SMOOTH = "R_ELEC_SMOOTH";
    }

    public class JYMouldInfo 
    {
        /// <summary>
        /// 模号
        /// </summary>
         [DisplayName("模号")]
        public string MODEL_NUMBER { get; set; }
        /// <summary>
        /// 件号
        /// </summary>
        [DisplayName("件号")]
        public string MR_NUMBER { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        [DisplayName("材质")]
        public string MR_MATERAL { get; set; }

        [NonSerialized]
        public Snap.NX.Body MouldBody;
    }

    /// <summary>
    /// 跑位信息
    /// </summary>
    public class JYPositioningInfo 
    {
        [DisplayName("跑位X")]
        public double X { get; set; }
        [DisplayName("跑位Y")]
        public double Y { get; set; }
        [DisplayName("跑位Z")]
        public double Z { get; set; }
        [NonSerialized]
        public double Rotation;
        [DisplayName("C角")]
        public string C { get; set; }

        [NonSerialized]
        public Snap.Vector FaceDir;
        [NonSerialized]
        public List<Snap.Position> Lines = new List<Snap.Position>();
        [NonSerialized]
        public List<NXOpen.NXObject> NXObjects = new List<NXOpen.NXObject>();
    }
    public class JYElecInfo
    {
        [DisplayName("电极名称")]
        public string ElecName { get; set; }

        [DisplayName("精工数量")]
        public double ELEC_FINISH_NUMBER { get; set; }

        [DisplayName("精工火花位")]
        public double ELEC_FINISH_SPACE { get; set; }

        [DisplayName("中工数量")]
        public double ELEC_MIDDLE_NUMBER { get; set; }

        [DisplayName("中工火花位")]
        public double ELEC_MIDDLE_SPACE { get; set; }

        [DisplayName("粗工数量")]
        public double ELEC_ROUGH_NUMBER { get; set; }

        [DisplayName("粗工火花位")]
        public double ELEC_ROUGH_SPACE { get; set; }

        [DisplayName("材质")]
        public string ELEC_MAT_NAME { get; set; }

        [DisplayName("夹具类型")]
        public string CLAMP_GENERAL_TYPE { get; set; }
        [DisplayName("电极类型")]
        public string ELEC_MACH_TYPE { get; set; }
        public string F_ELEC_SMOOTH { get; set; }
        public string M_ELEC_SMOOTH { get; set; }
        public string R_ELEC_SMOOTH { get; set; }

        /// <summary>
        /// 物料X
        /// </summary>
        public double SPECL { get; set; }
        /// <summary>
        /// 物料Y
        /// </summary>
        public double SPECW { get; set; }
        /// <summary>
        ///  物料Z
        /// </summary>

        public double SPECH { get; set; }

        [NonSerialized]
        public List<JYPositioningInfo> PositioningInfos = new List<JYPositioningInfo>();
    }
}
