using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonInterface
{
    /// <summary>
    /// EDM图纸接口
    /// </summary>
    public interface IEDM
    {
        void CreateDrawingSheet(List<ElecManage.PositioningInfo> ps, Snap.NX.Body steel);
    }

    public abstract class FtpHelper
    {
        public static void FtpUpload(string type, ElecManage.MouldInfo steelInfo, string fileName, string partName,EactConfig.ConfigData ConfigData)
        {
            var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
            string sToPath = string.Format("{0}/{1}/{2}", type, steelInfo.MODEL_NUMBER, partName);
            switch (ConfigData.FtpPathType)
            {
                case 1:
                    {
                        var extension = Path.GetExtension(fileName).ToUpper();
                        if (extension.Contains("STP") || extension.Contains("TXT"))
                        {

                        }
                        else
                        {
                            sToPath = string.Format("{0}/{1}", type, steelInfo.MODEL_NUMBER, partName);
                        }
                        break;
                    }
                case 2:
                    {
                        sToPath = string.Format("{0}/{1}", type, steelInfo.MODEL_NUMBER, partName);
                        break;
                    }
            }
            if (!EACTFTP.DirectoryExist(sToPath))
            {
                EACTFTP.MakeDirPath(sToPath);
            }

            EACTFTP.NextDirectory(sToPath);
            EACTFTP.UpLoadFile(fileName);
        }
    }
}
