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

    public abstract class DatabaseHelper
    {
        public static string GetConnStr(EactConfig.ConfigData data)
        {
            var connStr = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", data.DataBaseInfo.IP, data.DataBaseInfo.Name, data.DataBaseInfo.User, data.DataBaseInfo.Pass);
            return connStr;
        }
    }

    public enum EACTEdition
    {
        DEFAUT,
        PZ,
        HTUP,
        BX,
        YC
    }

    public struct FtpTypeConst
    {
        public const string CNC = "CNC";
    }

    public abstract class FtpHelper
    {
        public static void FtpUpload(string type, ElecManage.MouldInfo steelInfo, string fileName, string partName, EactConfig.ConfigData ConfigData)
        {
            string sToPath = string.Format("{0}/{1}/{2}", type, steelInfo.MODEL_NUMBER, partName);
            var extension = Path.GetExtension(fileName).ToUpper();
            switch (ConfigData.FtpPathType)
            {
                case 1:
                    {
                        if (extension.Contains("STP") || extension.Contains("TXT") || extension.Contains("PDF"))
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

            if ((ConfigData.Edition == (int)EACTEdition.HTUP || ConfigData.Edition == (int)EACTEdition.YC) && extension.Contains("PDF"))
            {
                sToPath = string.Format("{0}/{1}", type, steelInfo.MODEL_NUMBER, partName);
            }

            if (ConfigData.Edition == (int)EACTEdition.HTUP
                && type == FtpTypeConst.CNC
                && !string.IsNullOrEmpty(ConfigData.FileLocalDir)
                )
            {
                var tempPath = sToPath.Split('/').ToList();
                if (tempPath.Count > 0)
                {
                    tempPath = tempPath.Skip(1).Take(tempPath.Count - 1).ToList();
                }
                var path = Path.Combine(ConfigData.FileLocalDir, string.Join(@"\", tempPath.ToArray()));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.Copy(fileName, Path.Combine(path, Path.GetFileName(fileName)),true);
            }
            else
            {
                var EACTFTP = FlieFTP.Entry.GetFtp(ConfigData.FTP.Address, "", ConfigData.FTP.User, ConfigData.FTP.Pass, false);
                if (!EACTFTP.DirectoryExist(sToPath))
                {
                    EACTFTP.MakeDirPath(sToPath);
                }

                EACTFTP.NextDirectory(sToPath);
                EACTFTP.UpLoadFile(fileName);
            }
        }
    }
}
