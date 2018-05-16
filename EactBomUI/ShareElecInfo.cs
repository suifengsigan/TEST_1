using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EactBom
{
    public class ShareElecInfo
    {
        //public ShareElecInfo() { Checked = true; }

        ///// <summary>
        /////电极名称
        ///// </summary>
        //[DisplayName("选择")]
        //public bool Checked { get; set; }
        /// <summary>
        ///电极名称
        /// </summary>
        [DisplayName("模号")]
        public string MH { get { return EACT_CUPRUM.STEELMODELSN; } set { } }
        /// <summary>
        ///电极名称
        /// </summary>
        [DisplayName("件号")]
        public string JH { get { return EACT_CUPRUM.STEELMODULESN; } set { } }
        /// <summary>
        ///电极名称
        /// </summary>
        [DisplayName("电极编号")]
        public string CuprumSn { get { return EACT_CUPRUM.CUPRUMSN; } set { } }
        /// <summary>
        ///电极
        /// </summary>
        [NonSerialized]
        public DataAccess.Model.EACT_CUPRUM EACT_CUPRUM = new DataAccess.Model.EACT_CUPRUM();
    }
}
