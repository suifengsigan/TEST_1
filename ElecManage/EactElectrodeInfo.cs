using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

namespace ElecManage
{
    public class EactElectrodeInfo : ElectrodeInfo
    {
        public EactElectrodeInfo(Snap.NX.Body body) : base(body)
        {
        }

        public override int FINISH_NUMBER
        {
            get
            {
                var result = base.FINISH_NUMBER;
                if (result == 0)
                {
                    result=_body.GetAttrIntegerValue(XKElecConst.ELE_F_COUNT);
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

        public override string MAT_NAME
        {
            get
            {
                var result = base.MAT_NAME;
                if (string.IsNullOrEmpty(result))
                {
                    result = _body.GetAttrValue(XKElecConst.ELE_MATERIAL);
                }
                return result;
            }
            set => base.MAT_NAME = value;
        }
    }
}
