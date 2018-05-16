using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    /// <summary>
    /// 星空电极属性信息
    /// </summary>
    public class XKElectrodeInfo : ElectrodeInfo
    {
        protected XKElectrodeInfo() { }
        public XKElectrodeInfo(Snap.NX.Body body):base(body)
        {
            ELEC_FINISH_NUMBER = XKElecConst.ELE_F_COUNT;
            ELEC_MIDDLE_NUMBER = XKElecConst.ELE_S_COUNT;
            ELEC_ROUGH_NUMBER = XKElecConst.ELE_R_COUNT;
            ELEC_FINISH_SPACE = XKElecConst.ELE_F_SG;
            ELEC_MIDDLE_SPACE = XKElecConst.ELE_S_SG;
            ELEC_ROUGH_SPACE = XKElecConst.ELE_R_SG;
            ELEC_MAT_NAME = XKElecConst.ELE_MATERIAL;
        }
    }
}
