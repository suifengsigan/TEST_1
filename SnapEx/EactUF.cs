using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapEx
{
    public enum EACT_FeedUnit
    {
        FeedNone,
        FeedPerMinute,
        FeedPerRevolution
    }

    public struct EACT_Feedrate
    {
        public EACT_FeedUnit unit;
        public double value;
        public short color;
    }

    public abstract class EactUF
    {
        static double NxVersion = Helper.NxVersion;
        public static int UF_CUT_LEVELS_load(Tag oper_tag, ref NXOpen.UF.UFCutLevels.CutLevelsStruct[] value)
        {
            NXOpen.Utilities.JAM.StartUFCall();
            int errorCode;
            if (NxVersion >= 10)
            {
                errorCode = _CutterLevel_Load_libufun_cam(oper_tag, ref value);
            }
            else
            {
                errorCode = _CutterLevel_Load(oper_tag, ref value);
            }
            NXOpen.Utilities.JAM.EndUFCall();
            return errorCode;
        }

        public static EACT_Feedrate AskFeedRate(NXOpen.Tag operTag, int param_index)
        {
            NXOpen.Utilities.JAM.StartUFCall();
            EACT_Feedrate result;
            int errorCode;
            if (NxVersion >= 10)
            {
                errorCode = _AskFeedRate_libufun_cam(operTag, param_index, out result);
            }
            else
            {
                errorCode = _AskFeedRate(operTag, param_index, out result);
            }
            NXOpen.Utilities.JAM.EndUFCall();
            return result;
        }

        public static int SetFeedRate(NXOpen.Tag operTag, int param_index, double value)
        {
            var feedRate = AskFeedRate(operTag, param_index);
            feedRate.value = value;
            NXOpen.Utilities.JAM.StartUFCall();
            int errorCode;
            if (NxVersion >= 10)
            {
                errorCode = _SetFeedRate_libufun_cam(operTag, param_index, feedRate);
            }
            else
            {
                errorCode = _SetFeedRate(operTag, param_index, feedRate);
            }
            NXOpen.Utilities.JAM.EndUFCall();
            return errorCode;
        }


        [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "UF_PARAM_ask_subobj_ptr_value", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _AskFeedRate(Tag param_tag, int param_index, out EACT_Feedrate value);

        [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "UF_PARAM_set_subobj_ptr_value", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _SetFeedRate(Tag param_tag, int param_index, EACT_Feedrate value);
        [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "UF_CUT_LEVELS_load", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _CutterLevel_Load(Tag oper_tag, ref NXOpen.UF.UFCutLevels.CutLevelsStruct[] value);



        //UG10 版本调用dll位置不同
        [System.Runtime.InteropServices.DllImport("libufun_cam.dll", EntryPoint = "UF_PARAM_ask_subobj_ptr_value", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _AskFeedRate_libufun_cam(Tag param_tag, int param_index, out EACT_Feedrate value);

        [System.Runtime.InteropServices.DllImport("libufun_cam.dll", EntryPoint = "UF_PARAM_set_subobj_ptr_value", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _SetFeedRate_libufun_cam(Tag param_tag, int param_index, EACT_Feedrate value);
        [System.Runtime.InteropServices.DllImport("libufun_cam.dll", EntryPoint = "UF_CUT_LEVELS_load", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _CutterLevel_Load_libufun_cam(Tag oper_tag, ref NXOpen.UF.UFCutLevels.CutLevelsStruct[] value);

    }
}
