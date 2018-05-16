using SLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SLM_HANDLE_INDEX = System.UInt32;

namespace License
{
    public class Sense5Dev
    {
         
        #region  Verification

        public static bool Verification(out string StrMsg)
        {
            UInt32 Ret = SSErrCode.SS_OK;
            StrMsg = string.Empty;
            SLM_HANDLE_INDEX Handle = 0;

            try
            {
                string psd = "06AAA67F3CE60AEAB761A4CAEA3FF33D";
                Ret = SlmRuntime.SlmInitEasy(psd);
                if (Ret != SSErrCode.SS_OK)
                {
                    StrMsg = string.Format("05 - 加密锁认证失败:0x{0:X8}", Ret);
                    //MessageDxUtil.ShowMsg(StrMsg, MessageDxUtil.MsgType.Warning);
                    return false;
                }

                //
                //02. FIND LICENSE
                //
                IntPtr FindLic = SlmRuntime.SlmFindLicenseEasy(500, INFO_FORMAT_TYPE.JSON);
                Ret = SlmRuntime.SlmGetLastError();
                if (Ret != SSErrCode.SS_OK)
                {
                    StrMsg = string.Format("05 - 加密锁获取LICENSE失败:0x{0:X8}", Ret);
                    //MessageDxUtil.ShowMsg(StrMsg, MessageDxUtil.MsgType.Warning);
                    return false;
                }

                //
                //03. LOGIN
                //
                ST_LOGIN_PARAM stLogin = new ST_LOGIN_PARAM();
                stLogin.size = (UInt32)Marshal.SizeOf(stLogin);
                stLogin.license_id = 500;
                System.IntPtr stLoginPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(stLogin));
                Marshal.StructureToPtr(stLogin, stLoginPtr, false);

                Handle = SlmRuntime.SlmLoginEasy(stLoginPtr, INFO_FORMAT_TYPE.STRUCT);
                Ret = SlmRuntime.SlmGetLastError();
                if (Ret != SSErrCode.SS_OK)
                {
                    StrMsg = string.Format("05 - 加密锁登录失败:0x{0:X8}", Ret);
                    //MessageDxUtil.ShowMsg(StrMsg, MessageDxUtil.MsgType.Warning);
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                StrMsg = "05 - 加密锁验证失败！错误：" + ex.Message.ToString();
                //MessageDxUtil.ShowMsg("05 - 加密锁验证失败！错误：" + ex.Message.ToString(), MessageDxUtil.MsgType.Warning);
                return false;
            }
        }

        #endregion

    }
}
