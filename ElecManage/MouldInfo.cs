using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ElecManage
{
    /// <summary>
    /// 模仁信息
    /// </summary>
    public class MouldInfo
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

        [NonSerialized]
        public Snap.Position Origin = new Snap.Position();
        [NonSerialized]
        public Snap.Orientation Orientation = Snap.Orientation.Identity;
        /// <summary>
        /// 镶件体
        /// </summary>
        [NonSerialized]
        public List<Snap.NX.Body> SInsertBodies = new List<Snap.NX.Body>();
    }
}
