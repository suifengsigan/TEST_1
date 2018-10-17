using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;
using System.ComponentModel;
using System.Collections;
using System.Net.Security;
using EnterpriseDT.Net.Ftp;
using System.Linq;

namespace FlieFTP
{
    public class EACTFTP : FlieFTP.IEACTFTP
    {
        #region   <成员变量>

        private EnterpriseDT.Net.Ftp.FTPConnection ftpConnection = new EnterpriseDT.Net.Ftp.FTPConnection();

        public string FtpURI
        {
            get { return ftpConnection.GetURL(); }
        }


        #endregion

        #region   关闭FTP
        /// <summary>
        /// 关闭FTP
        /// </summary>
        public void CloseFTP()
        {
            if (ftpConnection.IsConnected)
            {
                ftpConnection.Close();
            }
        }

        #endregion

        #region   <FTP初始化>

        ~EACTFTP()
        {
            try
            {
                CloseFTP();
            }
            catch { }
        }

        /// <summary>
        /// FTP初始化
        /// </summary>
        /// <param name="FtpServerIP">FTP连接地址</param>
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="FtpUserID">用户名</param>
        /// <param name="FtpPassword">密码</param>
        public EACTFTP(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, bool SSL)
        {
            try
            {
                var strs = FtpServerIP.Split(':');
                //预制参数
                ftpConnection.StrictReturnCodes = true;
                ftpConnection.Timeout = 0;
                ftpConnection.TransferBufferSize = 4096;
                ftpConnection.TransferNotifyInterval = ((long)(4096));
                //设定参数
                ftpConnection.ServerAddress = strs[0];
                ftpConnection.ServerPort = int.Parse(strs[1]);
                ftpConnection.UserName = FtpUserID;
                ftpConnection.Password = FtpPassword;

                ftpConnection.CommandEncoding = Encoding.GetEncoding("GBK");

                if (System.Configuration.ConfigurationManager.AppSettings["rgTransferMode"] == "0")
                {
                    ftpConnection.CommandEncoding = Encoding.ASCII;
                    ftpConnection.DataEncoding = Encoding.ASCII;
                    ftpConnection.TransferType = FTPTransferType.ASCII;
                }

                ftpConnection.ConnectMode = FTPConnectMode.PASV;
                ftpConnection.Connect();
                if (FtpRemotePath != string.Empty)
                {
                    NextDirectory(FtpRemotePath);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// FTP初始化
        /// </summary>
        /// <param name="FtpServerIP">FTP连接地址</param>
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="FtpUserID">用户名</param>
        /// <param name="FtpPassword">密码</param>
        public EACTFTP(string FtpServerIP, int FtpServerPort, string FtpRemotePath, string FtpUser, string FtpPassword)
        {
            try
            {
                //预制参数
                ftpConnection.StrictReturnCodes = true;
                ftpConnection.Timeout = 0;
                ftpConnection.TransferBufferSize = 4096;
                ftpConnection.TransferNotifyInterval = ((long)(4096));
                //设定参数
                ftpConnection.ServerAddress = FtpServerIP;
                ftpConnection.ServerPort = FtpServerPort;
                ftpConnection.UserName = FtpUser;
                ftpConnection.Password = FtpPassword;
                ftpConnection.CommandEncoding = Encoding.GetEncoding("GBK");
                ftpConnection.ConnectMode = FTPConnectMode.PASV;
                ftpConnection.Connect();
                if (FtpRemotePath != string.Empty)
                {
                    NextDirectory(FtpRemotePath);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   切换到下级目录
        /// <summary>
        /// 切换到下级目录,用\\进行目录分割，例如CNC\\FCYA1
        /// </summary>
        /// <param name="sFolderName"></param>
        public void NextDirectory(string sFolderName)
        {
            sFolderName = sFolderName.Replace("/", "\\");
            if (ftpConnection.ChangeWorkingDirectory(sFolderName) == false)
            {
                throw new Exception("没有发现对应下级目录, 切换失败！");
            }
        }

        /// <summary>
        /// 切换到上级目录
        /// </summary>
        /// <param name="sFolderName"></param>
        public void UpDirectory()
        {
            if (ftpConnection.ChangeWorkingDirectoryUp() == false)
            {
                throw new Exception("没有发现对应上级目录, 切换失败！");
            }
        }

        #endregion

        #region   <上传文件和文件夹>

        /// <summary>
        /// 上传文件夹(远程当前目录)
        /// </summary>
        /// <param name="sFolderPath">需要上传的文件夹路径</param>
        /// <param name="sFolderName">文件夹名称</param>
        public void UploadFolder(string sFolderPath)
        {
            UploadFolder(sFolderPath, ftpConnection.ServerDirectory);
        }

        /// <summary>
        /// 上传文件夹（指定远程目录）
        /// </summary>
        /// <param name="localFolderPath">本地文件夹路径</param>
        /// <param name="remotePath">远程目录</param>
        public void UploadFolder(string localFolderPath, string remotePath)
        {
            DirectoryInfo dir = new DirectoryInfo(localFolderPath);
            var remoteDir = remotePath + "/" + dir.Name;
            //首先定位有没有这个文件夹
            if (DirectoryExist(remoteDir))
            {
                //如果当前路径下有这个文件夹，删除服务器上的我呢减价
                DeleteFtpDirWithAll(remoteDir);
            }
            _UploadFolder(localFolderPath, remotePath);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        public void UpLoadFile(string sFileName, string remotePath)
        {
            FileInfo fileInf = new FileInfo(sFileName);
            var remoteFile = remotePath + "/" + fileInf.Name;
            //首先定位有没有这个文件
            if (FileExist(remoteFile))
            {
                //如果当前路径下有这个文件夹，删除服务器上的文件
                Delete(remoteFile);
            }
            _UploadFile(fileInf.FullName, remoteFile);
        }

        /// <summary>
        /// 上传文件(当前远程工作路径)
        /// </summary>
        /// <param name="sFileName"></param>
        public void UpLoadFile(string sFileName)
        {
            try
            {
                FileInfo fileInf = new FileInfo(sFileName);
                //首先定位有没有这个文件
                if (FileExist(fileInf.Name))
                {
                    //如果当前路径下有这个文件夹，删除服务器上的文件
                    Delete(fileInf.Name);
                }
                _UploadFile(fileInf.FullName, fileInf.Name);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 上传文件夹（指定远程目录）
        /// </summary>
        /// <param name="localFolderPath">本地文件夹路径</param>
        /// <param name="remotePath">远程目录</param>
        public void _UploadFolder(string localFolderPath, string remotePath)
        {
            try
            {
                string ex = remotePath + "/";
                DirectoryInfo dir = new DirectoryInfo(localFolderPath);
                //创建目录
                ftpConnection.CreateDirectory(ex + dir.Name);
                //在服务器上新建这个文件
                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    //上传文件
                    _UploadFile(fi.FullName, ex + dir.Name + "/" + fi.Name);
                }
                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    _UploadFolder(d.FullName, ex + dir.Name);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sFileName">上传文件的路径</param>
        /// <param name="sFTPURI">FTP服务器目标地址</param>
        /// <param name="backgroundWorker"></param>
        private void _UploadFile(string sFilePath, string sFileName)
        {
            try
            {
                ftpConnection.UploadFile(sFilePath, sFileName);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   <下载文件夹和文件>
        /// <summary>
        /// 下载文件夹(当前选择)
        /// </summary>
        /// <param name="sFolderPath">下载文件夹保存到的本地路径</param>
        /// <param name="sFolderName">需要下载的文件夹名称（服务器端文件夹名称）</param>
        void _DownloadFolder(string sFolderPath, string sFolderName, string remotePath = null)
        {
            try
            {
                if (!Directory.Exists(sFolderPath))
                {
                    Directory.CreateDirectory(sFolderPath);
                }

                if (remotePath != null)
                {
                    sFolderName = remotePath + "/" + sFolderName;
                }

                var FolderList = GetDirectoryList(sFolderName);
                var FileList = GetFileList(sFolderName);
                //查询一下是否有这个目录了
                foreach (var FL in FileList)
                {
                    if (FL != "." && FL != "..")
                    {
                        DownloadFile(sFolderPath, sFolderName, FL);
                    }
                }
                foreach (var FD in FolderList)
                {
                    if (FD != "." && FD != "..")
                    {
                        _DownloadFolder(Path.Combine(sFolderPath, FD), FD, sFolderName);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 下载文件夹
        /// </summary>
        /// <param name="sFolderPath">下载文件夹保存到的本地路径</param>
        /// <param name="sFolderName">需要下载的文件夹名称（服务器端文件夹名称）</param>
        public void DownloadFolder(string sFolderPath, string sFolderName)
        {
            _DownloadFolder(sFolderPath, sFolderName, ftpConnection.ServerDirectory);
        }

        /// <summary>
        /// 下载文件夹
        /// </summary>
        /// <param name="sFolderPath">下载文件夹保存到的本地路径</param>
        /// <param name="sFolderName">需要下载的文件夹名称（服务器端文件夹名称）</param>
        public void DownloadFolder(string sFolderPath, string remotePath, string sFolderName)
        {
            _DownloadFolder(sFolderPath, sFolderName, remotePath);
        }

        public void DownloadFile(string localPath, string remotePath, string remoteFile)
        {
            try
            {
                var array = ftpConnection.DownloadByteArray(remotePath + "/" + remoteFile);
                File.WriteAllBytes(Path.Combine(localPath, remoteFile), array);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="sFileName"></param>
        public void DownloadFile(string sFilePath, string sFileName)
        {
            try
            {
                ftpConnection.DownloadFile(sFilePath, sFileName);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   <删除文件>
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName)
        {
            try
            {
                ftpConnection.DeleteFile(fileName);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   删除文件夹(文件夹不为空)

        /// <summary>
        /// 删除文件夹(文件夹不为空)
        /// </summary>
        /// <param name="sFolderName">文件夹名称</param>
        /// <param name="SFTPPath">FTP服务器路径</param>
        public void DeleteFtpDirWithAll(string sFolderName, bool isDeleteFolder = true)
        {
            try
            {
                var fileInfos = ftpConnection.GetFileInfos(sFolderName);
                foreach (var fileInfo in fileInfos)
                {
                    if (fileInfo.Name != "." && fileInfo.Name != "..")
                    {
                        if (fileInfo.Dir)
                            DeleteFtpDirWithAll(sFolderName + "/" + fileInfo.Name, true);
                        else
                            ftpConnection.DeleteFile(sFolderName + "/" + fileInfo.Name);
                    }
                }
                if (isDeleteFolder)
                {
                    ftpConnection.DeleteDirectory(sFolderName);
                }

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   删除文件夹(空文件夹)
        /// <summary>
        /// 删除文件夹(空文件夹)
        /// </summary>
        /// <param name="sFolderName"></param>
        public void RemoveDirectory(string folderName)
        {
            try
            {
                ftpConnection.DeleteDirectory(folderName);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region   获取当前目录下明细(包含文件和文件夹)
        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            try
            {
                var Finfos = ftpConnection.GetFileInfos();
                var RF = string.Empty;
                foreach (var F in Finfos)
                {
                    if (F.Name != "." && F.Name != "..")
                    {
                        RF += F.Name + "*";
                    }
                }
                if (RF == string.Empty)
                {
                    return new string[] { };
                }
                else
                {
                    RF = RF.TrimEnd('*');
                    return RF.Split('*');
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   获取当前目录下文件列表(仅文件)
        /// <summary>
        /// 获取当前目录下文件列表(仅文件)
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            try
            {
                return ftpConnection.GetFiles();
            }
            catch
            {
                throw;
            }
        }

        public string[] GetFileListEx(string LocaPath)
        {
            try
            {
                var d = ftpConnection.GetFileInfos(LocaPath).Where(u => !u.Dir).OrderBy(u=>u.LastModified);
                return Enumerable.Select(d, u => u.Name).ToArray();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 在远端路径查找一个文件
        /// </summary>
        /// <param name="LocaPath">用\\进行目录分割，例如CNC\\FCYA1</param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public string[] GetFileList(string LocaPath)
        {
            return GetFileListEx(LocaPath);
            //try
            //{
            //    var result = new List<string>();
            //    var d = ftpConnection.GetFileInfos(LocaPath).ToList();
            //    d.ForEach(u =>
            //    {
            //        if (!u.Dir)
            //        {
            //            result.Add(u.Name);
            //        }
            //    });
            //    return result.ToArray();
            //}
            //catch
            //{
            //    throw;
            //}
        }

        #endregion

        #region   获取当前目录下所有的文件夹列表(仅文件夹)

        /// <summary>
        /// 获取当前目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetDirectoryList()
        {
            return GetDirectoryList(ftpConnection.ServerDirectory);
        }

        /// <summary>
        /// 获取设定目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <param name="LocaPath">设定路径</param>
        /// <returns></returns>
        public string[] GetDirectoryList(string LocaPath)
        {
            try
            {
                var Finfos = ftpConnection.GetFileInfos(LocaPath);
                var RF = string.Empty;
                foreach (var F in Finfos)
                {
                    if (F.Dir == true && F.Name != "." && F.Name != "..")
                    {
                        RF += F.Name + "*";
                    }
                }
                if (RF == string.Empty)
                {
                    return new string[] { };
                }
                else
                {
                    RF = RF.TrimEnd('*');
                    return RF.Split('*');
                }
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region   判断当前目录下指定的子目录是否存在
        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            try
            {
                return ftpConnection.DirectoryExists(RemoteDirectoryName);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string LocaPath, string RemoteDirectoryName)
        {
            try
            {
                var dirList = ftpConnection.GetFileInfos(LocaPath);
                foreach (var str in dirList)
                {
                    if (str.Dir == true && str.Name == RemoteDirectoryName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region   判断当前目录下指定的文件是否存在
        /// <summary>
        /// 判断当前目录下指定的文件是否存在
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        public bool FileExist(string RemoteFileName)
        {
            try
            {
                //EACTSYSLOG.EACTSYSLOG.EactTrace("RemoteFileName", RemoteFileName);
                return ftpConnection.Exists(RemoteFileName);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 判断远端目录下是否有这个文件
        /// </summary>
        /// <param name="LocaPath"></param>
        /// <param name="RemoteFileName"></param>
        /// <returns></returns>
        public bool FileExist(string LocaPath, string RemoteFileName)
        {
            return FileExist(LocaPath + "/" + RemoteFileName);
        }

        #endregion

        #region   创建文件夹

        /// <summary>
        /// 创建一串文件夹
        /// </summary>
        /// <param name="DirPath">路径格式："aa/bb/cc/ee/ff"</param>
        /// <returns></returns>
        public void MakeDirPath(string DirPath)
        {
            //首先查找路径中有没有/
            try
            {
                if (DirPath.IndexOf('/') < 0)
                {
                    if (!DirectoryExist(DirPath))
                    {
                        MakeDir(DirPath);
                    }
                }
                else
                {
                    var Path = DirPath.Split('/');
                    var PathName = string.Empty;
                    foreach (var P in Path)
                    {
                        //首先判断这个路径下有没有这个文件夹
                        if (!DirectoryExist(PathName, P))
                        {
                            PathName += P + "/";
                            MakeDir(PathName);
                        }
                        else
                        {
                            PathName += P + "/";
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            try
            {
                ftpConnection.CreateDirectory(dirName);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region   获取指定文件大小
        /// <summary>
        /// 获取指定文件大小
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            try
            {
                var FInfos = ftpConnection.GetFileInfos();
                long FS = 0;
                foreach (var F in FInfos)
                {
                    if (F.Dir == false && F.Name == filename)
                    {
                        FS = F.Size;
                        break;
                    }
                }
                return FS;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region  改名

        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void ReName(string currentFilename, string newFilename)
        {
            try
            {
                ftpConnection.RenameFile(currentFilename, newFilename);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}