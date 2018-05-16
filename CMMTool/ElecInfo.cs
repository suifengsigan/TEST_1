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
        /// 获取基准角法向
        /// </summary>
        public static Snap.Vector GetBaseCornerDirection(Snap.Vector director,Snap.NX.Body body,Snap.Position basePoint) 
        {
            var vec = new Snap.Vector();
            director = director.Copy(Snap.Geom.Transform.CreateRotation(basePoint, 90));

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

            if (baseFace != null) 
            {
                faces.Remove(baseFace);
                faces.ForEach(u =>
                {
                    if (u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane
                        && u.Box.MaxZ > basePoint.Z
                        && SnapEx.Helper.Equals(u.GetFaceDirection(), director)
                         && SnapEx.Helper.Equals(u.GetFaceDirection(), -director)
                        )
                    {
                        u.Color = System.Drawing.Color.Red;
                    }
                });
            }

            return vec;
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
        [DisplayName("旋转")]
        public double Rotation { get; set; }

        private List<Snap.Position> line = new List<Snap.Position>();
        public List<Snap.Position> GetLine() { return line;}
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

        private List<JYPositioningInfo> positioningInfos = new List<JYPositioningInfo>();

        public List<JYPositioningInfo> GetPositioningInfos() { return positioningInfos; }

    }
}
