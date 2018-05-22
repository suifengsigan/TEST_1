using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FtpLib;
using System.IO;

namespace TOOL
{
    public class FtpLib : FlieFTP.IEACTFTP
    {
        protected FtpConnection ftpConnection;

         /// <summary>
        /// FTP初始化
        /// </summary>
        /// <param name="FtpServerIP">FTP连接地址</param>
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="FtpUserID">用户名</param>
        /// <param name="FtpPassword">密码</param>
        public FtpLib(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, bool SSL)
        {
            try
            {
                var strs = FtpServerIP.Split(':');
                ftpConnection = new FtpConnection(strs[0], int.Parse(strs[1]), FtpUserID, FtpPassword);
                ftpConnection.Open(); /* Open the FTP connection */
                ftpConnection.Login(); /* Login using previously provided credentials */
                NextDirectory(FtpRemotePath);
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
        public FtpLib(string FtpServerIP, int FtpServerPort, string FtpRemotePath, string FtpUser, string FtpPassword)
        {
            try
            {
                ftpConnection = new FtpConnection(FtpServerIP, FtpServerPort, FtpUser, FtpPassword);
                ftpConnection.Open(); /* Open the FTP connection */
                ftpConnection.Login(); /* Login using previously provided credentials */
                NextDirectory(FtpRemotePath);
            }
            catch
            {
                throw;
            }
        }

        ~FtpLib()
        {
            try
            {
                CloseFTP();
            }
            catch { }
        }


        /// <summary>
        /// 关闭FTP
        /// </summary>
        public void CloseFTP()
        {
            ftpConnection.Dispose();
        }

        /// <summary>
        /// 创建一串文件夹
        /// </summary>
        /// <param name="DirPath">路径格式："aa/bb/cc/ee/ff"</param>
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
                        if (!string.IsNullOrEmpty(P)) 
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


        /// <summary>
        /// 在远端路径查找一个文件
        /// </summary>
        public virtual string[] GetFileList(string LocaPath)
        {
            try
            {
                var result = new List<string>();
                ftpConnection.GetFiles(LocaPath).ToList().ForEach(u =>
                {
                    result.Add(u.Name);
                });
                return result.ToArray();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取设定目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <param name="LocaPath">设定路径</param>
        public virtual string[] GetDirectoryList(string LocaPath)
        {
            try
            {
                var directories = ftpConnection.GetDirectories(LocaPath);
                var strs = new List<string>();
                directories.ToList().ForEach(u => {
                    if (u.Name != "." && u.Name != "..")
                    {
                        strs.Add(u.Name);
                    }
                });
                return strs.ToArray();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            try
            {
                var result = string.Empty;
                var list = RemoteDirectoryName.Split('/').ToList();
                for (int i = 0; i < list.Count - 1; i++)
                {
                    result += string.Format("/{0}", list[i]);
                }
                return DirectoryExist(result,list.LastOrDefault());
            }
            catch
            {
                throw;
            }
        }

        public bool DirectoryExist(string LocaPath, string RemoteDirectoryName)
        {
            try
            {
                var dirList = GetDirectoryList(LocaPath);
                foreach (var str in dirList)
                {
                    if (str == RemoteDirectoryName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 切换到下级目录,用\\进行目录分割，例如CNC\\FCYA1
        /// </summary>
        /// <param name="sFolderName"></param>
        public void NextDirectory(string sFolderName)
        {
            try
            {
                ftpConnection.SetCurrentDirectory(Combine(ftpConnection.GetCurrentDirectory(),sFolderName));
            }
            catch
            {
                throw new Exception(string.Format("没有发现对应下级目录{0}, 切换失败！",sFolderName));
            }
        }

        /// <summary>
        /// 上传文件(当前远程工作路径)
        /// </summary>
        /// <param name="sFileName"></param>
        public void UpLoadFile(string sFileName)
        {
            UpLoadFile(sFileName, ftpConnection.GetCurrentDirectory());
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        public virtual void UpLoadFile(string sFileName, string remotePath)
        {
            try
            {
                FileInfo fileInf = new FileInfo(sFileName);
                var remoteFile = Combine(remotePath, fileInf.Name);
                if (ftpConnection.FileExists(remoteFile))
                {
                    Delete(remoteFile);
                }
                ftpConnection.PutFile(sFileName, remoteFile);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 上传文件夹(远程当前目录)
        /// </summary>
        /// <param name="sFolderPath">需要上传的文件夹路径</param>
        /// <param name="sFolderName">文件夹名称</param>
        public void UploadFolder(string sFolderPath)
        {
            UploadFolder(sFolderPath, ftpConnection.GetCurrentDirectory());
        }

        /// <summary>
        /// 上传文件夹（指定远程目录）
        /// </summary>
        /// <param name="localFolderPath">本地文件夹路径</param>
        /// <param name="remotePath">远程目录</param>
        public virtual void UploadFolder(string localFolderPath, string remotePath)
        {
            DirectoryInfo dir = new DirectoryInfo(localFolderPath);
            var remoteDir = Combine(remotePath, dir.Name);
            //首先定位有没有这个文件夹
            if (DirectoryExist(remoteDir))
            {
                //如果当前路径下有这个文件夹，删除服务器上的我呢减价
                DeleteFtpDirWithAll(remoteDir);
            }
            _UploadFolder(localFolderPath, remotePath);
        }

        /// <summary>
        /// 上传文件夹（指定远程目录）
        /// </summary>
        /// <param name="localFolderPath">本地文件夹路径</param>
        /// <param name="remotePath">远程目录</param>
       void _UploadFolder(string localFolderPath, string remotePath)
        {
            try
            {
                string ex = remotePath;
                DirectoryInfo dir = new DirectoryInfo(localFolderPath);
                //创建目录
                ftpConnection.CreateDirectory(Combine(ex, dir.Name));
                //在服务器上新建这个文件
                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    //上传文件
                    UpLoadFile(fi.FullName, Combine(ex, dir.Name));
                }
                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    _UploadFolder(d.FullName, Combine(ex, dir.Name));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public virtual void Delete(string fileName)
        {
            try
            {
                ftpConnection.RemoveFile(fileName);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除文件夹(文件夹不为空)
        /// </summary>
        /// <param name="sFolderName">文件夹名称</param>
        /// <param name="SFTPPath">FTP服务器路径</param>
        public virtual void DeleteFtpDirWithAll(string sFolderName, bool isDeleteFolder = true)
        {
            try
            {
                //EACTSYSLOG.EACTSYSLOG.EactTrace("DeleteFtpDirWithAll", sFolderName);
                GetDirectoryList(sFolderName).ToList().ForEach(u =>
                {
                    DeleteFtpDirWithAll(Combine(sFolderName, u), true);
                });

                GetFileList(sFolderName).ToList().ForEach(u =>
                {
                    var file = Combine(sFolderName, u);
                    //EACTSYSLOG.EACTSYSLOG.EactTrace("DeleteFile", file);
                    Delete(file);
                });

                if (isDeleteFolder)
                {
                    //EACTSYSLOG.EACTSYSLOG.EactTrace("DeleteDirectory", sFolderName);
                    ftpConnection.RemoveDirectory(sFolderName);
                }

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public void DownloadFile(string localPath, string remotePath, string remoteFile)
        {
            try
            {
                ftpConnection.GetFile(Combine(remotePath, remoteFile),Path.Combine(localPath, remoteFile),false);
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
        public void DownloadFolder(string sFolderPath, string remotePath, string sFolderName)
        {
            _DownloadFolder(sFolderPath, sFolderName, remotePath);
        }

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
                    sFolderName = Combine(remotePath, sFolderName);
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
      

        public string Combine(string remotePath, string RemoteFile)
        {
            return remotePath.TrimEnd('/') + "/" + RemoteFile.TrimStart('/');
        }
    }
}
