using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlieFTP
{
    public class Entry
    {
        public static IEACTFTP GetFtp(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, bool SSL)
        {
            var ftp = new EACTFTP(FtpServerIP, FtpRemotePath, FtpUserID, FtpPassword,SSL);
            return ftp;
        }
    }
}
