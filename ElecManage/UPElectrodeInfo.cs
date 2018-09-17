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
            ELEC_MAT_NAME = "UP_ELECTRODE_FINE_MATERIAL";
            ELEC_F_MAT_NAME = "UP_ELECTRODE_FINE_MATERIAL";
            ELEC_M_MAT_NAME = "UP_ELECTRODE_SUITABLE_MATERIAL";
            ELEC_R_MAT_NAME = "UP_ELECTRODE_SUITABLE_MATERIAL";
            ELEC_MACH_TYPE = "UP_ELECTRODE_EDM_POSITION";
        }


        public override string ElecCuttingSize(double blankstock, double matchJiajuValue)
        {
            var str = _body.GetAttrValue("UP_ELECTRODE_SPECIFICATION");
            var strs = str.Split('X').ToList();
            if (strs.Count >= 3)
            {
                return string.Format("{0}x{1}x{2}",
                        strs[0]
                        , strs[1]
                        , strs[2]
                        );
            }
            return base.ElecCuttingSize(blankstock, matchJiajuValue);
        }
    }
}
