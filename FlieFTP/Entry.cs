using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlieFTP
{
    public class Entry
    {
        public static IEACTFTP GetFtp(string FtpServerIP, int FtpServerPort, string FtpRemotePath, string FtpUser, string FtpPassword)
        {
            var ftp = new EACTFTP(FtpServerIP, FtpServerPort, FtpRemotePath, FtpUser, FtpPassword);
            return ftp;
        }
        public static IEACTFTP GetFtp(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, bool SSL)
        {
            var ftp = new EACTFTP(FtpServerIP, FtpRemotePath, FtpUserID, FtpPassword,SSL);
            return ftp;
        }

        /// <summary>
        /// 获取机台Ftp
        /// </summary>
        /// <returns></returns>
        public static IEACTFTP GetMachFtp(string FtpServerIP, int FtpServerPort, string FtpRemotePath, string FtpUser, string FtpPassword)
        {
            var ftp = new TOOL.FtpLibEx(FtpServerIP, FtpServerPort, FtpRemotePath, FtpUser, FtpPassword);
            return ftp;
        }

        /// <summary>
        /// 获取机台Ftp
        /// </summary>
        public static IEACTFTP GetMachFtp(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, bool SSL)
        {
            var ftp = new TOOL.FtpLibEx(FtpServerIP, FtpRemotePath, FtpUserID, FtpPassword, SSL);
            return ftp;
        }
    }
}
