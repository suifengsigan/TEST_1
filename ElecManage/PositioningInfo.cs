using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ElecManage
{
    /// <summary>
    /// 跑位信息
    /// </summary>
    public class PositioningInfo
    {
        [DisplayName("跑位X")]
        public double X { get; set; }
        [DisplayName("跑位Y")]
        public double Y { get; set; }
        [DisplayName("跑位Z")]
        public double Z { get; set; }

        [DisplayName("旋转")]
        public double C { get {
            return SnapEx.Helper.CAngle(QuadrantType, Entry.Instance.DefaultQuadrantType);
        } set { } }
      
        [DisplayName("象限角")]
        public string Quadrant
        {
            get
            {
                var type = QuadrantType;
                var result = "右上角";
                if (type == QuadrantType.Second)
                {
                    result = "左上角";
                }
                else if (type == QuadrantType.Three)
                {
                    result = "左下角";
                }
                else if (type == QuadrantType.Four)
                {
                    result = "右下角";
                }
                return result;
            }
        }

        [NonSerialized]
        public QuadrantType QuadrantType = QuadrantType.First;

        [NonSerialized]
        public double Rotation;
        [NonSerialized]
        public Electrode Electrode;
    }
}
