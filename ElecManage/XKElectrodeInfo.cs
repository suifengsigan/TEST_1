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

        public override int FINISH_NUMBER
        {
            get => base.FINISH_NUMBER;
            set => _body.SetStringAttribute(ELEC_FINISH_NUMBER,value.ToString());
        }

        public override int MIDDLE_NUMBER
        {
            get => base.MIDDLE_NUMBER;
            set => _body.SetStringAttribute(ELEC_MIDDLE_NUMBER, value.ToString());
        }

        public override int ROUGH_NUMBER
        {
            get => base.ROUGH_NUMBER;
            set => _body.SetStringAttribute(ELEC_ROUGH_NUMBER, value.ToString());
        }

        public override double FINISH_SPACE
        {
            get => base.FINISH_SPACE;
            set => _body.SetStringAttribute(ELEC_FINISH_SPACE, value.ToString());
        }

        public override double MIDDLE_SPACE
        {
            get => base.MIDDLE_SPACE;
            set => _body.SetStringAttribute(ELEC_MIDDLE_SPACE, value.ToString());
        }

        public override double ROUGH_SPACE
        {
            get => base.ROUGH_SPACE;
            set => _body.SetStringAttribute(ELEC_ROUGH_SPACE, value.ToString());
        }
    }
}
