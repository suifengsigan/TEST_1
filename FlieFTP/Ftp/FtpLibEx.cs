using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FtpLib;
using System.Net;
using System.IO;

namespace TOOL
{
    public class FtpLibEx : FtpLib
    {
        public FtpLibEx(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, bool SSL)
            : base(FtpServerIP, FtpRemotePath, FtpUserID, FtpPassword, SSL)
        { }
        public FtpLibEx(string FtpServerIP, int FtpServerPort, string FtpRemotePath, string FtpUser, string FtpPassword)
            : base(FtpServerIP, FtpServerPort, FtpRemotePath, FtpUser, FtpPassword)
        { }


        #region   获取当前目录下明细(包含文件和文件夹)
        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList(string local)
        {
            try
            {
                var list = new List<string>();
                var client = new EACT.FTPClient(ftpConnection.Host, local, ftpConnection.UserName, ftpConnection.Password, ftpConnection.Port);
                client.Connect();
                if (client.Connected)
                {
                    list = client.List(string.Empty).ToList();
                    client.DisConnect();
                }

                return list.ToArray();
            }
            catch
            {
                return new string[] { };
            }
        }

        #endregion

        public override string[] GetFileList(string LocaPath)
        {
            try
            {
                string[] drectory = GetFilesDetailList(LocaPath);
                var list = new List<string>();
                foreach (string str in drectory)
                {
                    if (str.Trim().Substring(0, 1).ToUpper() == "-")
                    {
                        /*判断 Unix 风格*/
                        string dir = str.Split(' ').Where(u => !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(u.Trim())).LastOrDefault();
                        AddList(list, dir);
                    }
                    else if (str.Trim().Substring(0, 1).ToUpper() != "D" && !str.Contains("<DIR>"))
                    {
                        var temp = str.Split(' ').Where(u => !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(u.Trim()));
                        if (temp.Count() == 4)
                        {
                            var dir = temp.LastOrDefault();
                            AddList(list, dir);
                        }
                    }
                }
                return list.ToArray();
            }
            catch
            {
                return new string[] { };
            }
        }

        public override string[] GetDirectoryList(string LocaPath)
        {
            try
            {
                string[] drectory = GetFilesDetailList(LocaPath);
                var list = new List<string>();
                foreach (string str in drectory)
                {
                    int dirPos = str.IndexOf("<DIR>");
                    if (dirPos > 0)
                    {
                        /*判断 Windows 风格*/
                        list.Add(str.Substring(dirPos + 5).Trim());
                    }
                    else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                    {
                        /*判断 Unix 风格*/
                        string dir = str.Split(' ').Where(u => !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(u.Trim())).LastOrDefault();
                        AddList(list, dir);

                    }
                }
                return list.ToArray();
            }
            catch
            {
                return new string[] { };
            }
        }

        void AddList(List<string> list,string dir) 
        {
            if (dir != null && !string.IsNullOrEmpty(dir.Trim()))
            {
                dir = dir.Trim();
                if (dir != "." && dir != "..")
                {
                    list.Add(dir);
                }
            }
        }

    }
}
