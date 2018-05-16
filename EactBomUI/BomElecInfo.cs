using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EactBom
{
    public class ViewElecInfo
    {
        public ViewElecInfo() { Checked = true; }
        [DisplayName("选择")]
        public bool Checked { get; set; }
        [DisplayName("电极名称")]
        public string ElectName { get; set; }
        [DisplayName("共用电极")]
        public string ShareElecStr { get { return ShareElecList.Count>0 ? "是" : "否"; } set { } }
        [NonSerialized]
        public List<Snap.NX.Body> Bodies = new List<Snap.NX.Body>();
        [NonSerialized]
        public List<DataAccess.Model.EACT_CUPRUM> ShareElecList = new List<DataAccess.Model.EACT_CUPRUM>();
        public bool ShareElec() { return ShareElecList.Count > 0; }
    }
}
