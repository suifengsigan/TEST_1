using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace License
{
    public class Entry
    {
        private Entry() { }
        public static Entry Instance = new Entry();
        public bool Verification(int SenseVar, out string strMsg)
        {
            #region   加密狗
            strMsg = string.Empty;
            if (!Debugger.IsAttached)
            {
                switch (SenseVar)
                {
                    case 0:
                        {
                            if (!Sense4Dev.Verification(out strMsg))
                            {
                                return false;
                            }
                        }
                        break;
                    case 1:
                        {
                            if (!Sense5Dev.Verification(out strMsg))
                            {
                                return false;
                            }
                        }
                        break;
                    default:
                        {
                            strMsg = "请正确配置加密锁的类型";
                            //MessageDxUtil.ShowMsg("请正确配置加密锁的类型", MessageDxUtil.MsgType.Warning);
                            return false;
                        }
                }
            }
            #endregion

            return true;
        }
    }
}
