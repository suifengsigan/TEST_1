using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    /// <summary>
    /// 优品
    /// </summary>
    public class UPElectrodeInfo : ElectrodeInfo
    {
        public UPElectrodeInfo(Snap.NX.Body body) : base(body)
        {
            ELEC_FINISH_NUMBER = "UP_ELECTRODE_FINE_QUANTITY";
            ELEC_MIDDLE_NUMBER = "UP_ELECTRODE_SUITABLE_QUANTITY";
            ELEC_ROUGH_NUMBER = "UP_ELECTRODE_ROUGH_QUANTITY";
            ELEC_FINISH_SPACE = "UP_ELECTRODE_FINE_SPACE";
            ELEC_MIDDLE_SPACE = "UP_ELECTRODE_SUITABLE_SPACE";
            ELEC_ROUGH_SPACE = "UP_ELECTRODE_ROUGH_SPACE";
            ELEC_MAT_NAME = "ELEC_F_MAT_NAME";
            ELEC_F_MAT_NAME = "UP_ELECTRODE_FINE_MATERIAL";
            ELEC_M_MAT_NAME = "UP_ELECTRODE_SUITABLE_MATERIAL";
            ELEC_R_MAT_NAME = "UP_ELECTRODE_SUITABLE_MATERIAL";
        }

        public override int FINISH_NUMBER
        {
            get
            {
                var result = base.FINISH_NUMBER;
                if (result == 0)
                {
                    result = _body.GetAttrIntegerValue(XKElecConst.ELE_F_COUNT);
                }
                return result;
            }
            set => base.FINISH_NUMBER = value;
        }

        public override int MIDDLE_NUMBER
        {
            get
            {
                var result = base.MIDDLE_NUMBER;
                if (result == 0)
                {
                    result = _body.GetAttrIntegerValue(XKElecConst.ELE_S_COUNT);
                }
                return result;
            }
            set => base.MIDDLE_NUMBER = value;
        }

        public override int ROUGH_NUMBER
        {
            get
            {
                var result = base.ROUGH_NUMBER;
                if (result == 0)
                {
                    result = _body.GetAttrIntegerValue(XKElecConst.ELE_R_COUNT);
                }
                return result;
            }
            set => base.ROUGH_NUMBER = value;
        }


        public override double FINISH_SPACE
        {
            get
            {
                var result = base.FINISH_SPACE;
                if (result == 0)
                {
                    result = _body.GetAttrRealValue(XKElecConst.ELE_F_SG);
                }
                return result;
            }
            set => base.FINISH_SPACE = value;
        }

        public override double MIDDLE_SPACE
        {
            get
            {
                var result = base.MIDDLE_SPACE;
                if (result == 0)
                {
                    result = _body.GetAttrRealValue(XKElecConst.ELE_S_SG);
                }
                return result;
            }
            set => base.MIDDLE_SPACE = value;
        }

        public override double ROUGH_SPACE
        {
            get
            {
                var result = base.ROUGH_SPACE;
                if (result == 0)
                {
                    result = _body.GetAttrRealValue(XKElecConst.ELE_R_SG);
                }
                return result;
            }
            set => base.ROUGH_SPACE = value;
        }
    }
}
