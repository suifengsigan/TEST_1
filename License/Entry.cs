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
        void CopyFile(string oldFile, string newFile,bool b) 
        {
            if (!File.Exists(newFile)) 
            {
                File.Copy(oldFile, newFile,b);
            }
        }
        public bool Verification(int SenseVar,out string strMsg) { 
#region   加密狗
            strMsg = string.Empty;
            if (!Debugger.IsAttached)
            {
                //var SenseVar = -1;
                //try
                //{
                //    SenseVar = int.Parse(ConfigurationManager.AppSettings["SenseVar"]);
                //}
                //catch
                //{
                //    MessageDxUtil.ShowMsg("请正确配置加密锁的类型", MessageDxUtil.MsgType.Warning);
                //    return;
                //}
                switch (SenseVar)
                {
                    case 0:
                        {
                            //if (IntPtr.Size == 8)
                            //{
                            //    CopyFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\EliteIVNet\\Win_X64\\Sense4user.dll", System.AppDomain.CurrentDomain.BaseDirectory + "\\Sense4user.dll", true);
                            //}
                            //else
                            //{
                            //    CopyFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\EliteIVNet\\Win_X86\\Sense4user.dll", System.AppDomain.CurrentDomain.BaseDirectory + "\\Sense4user.dll", true);
                            //}
                            if (!Sense4Dev.Verification(out strMsg))
                            {
                                return false;
                            }
                        }
                        break;
                    case 1:
                        {
                            //if (IntPtr.Size == 8)
                            //{
                            //    CopyFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\EliteVNet\\X64\\slm_runtime_easy.dll", System.AppDomain.CurrentDomain.BaseDirectory + "\\slm_runtime_easy.dll", true);
                            //}
                            //else
                            //{
                            //    CopyFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\EliteVNet\\X86\\slm_runtime_easy.dll", System.AppDomain.CurrentDomain.BaseDirectory + "\\slm_runtime_easy.dll", true);
                            //}
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
