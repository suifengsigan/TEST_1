using System;
namespace FlieFTP
{
    public interface IEACTFTP
    {
        //void CloseFTP();
        //string Combine(string remotePath, string RemoteFile);
        void Delete(string fileName);
        void DeleteFtpDirWithAll(string sFolderName, bool isDeleteFolder = true);
        bool DirectoryExist(string RemoteDirectoryName);
        bool DirectoryExist(string LocaPath, string RemoteDirectoryName);
        void DownloadFile(string localPath, string remotePath, string remoteFile);
        void DownloadFolder(string sFolderPath, string remotePath, string sFolderName);
        string[] GetDirectoryList(string LocaPath);
        string[] GetFileList(string LocaPath);
        void MakeDir(string dirName);
        void MakeDirPath(string DirPath);
        void NextDirectory(string sFolderName);
        void UpLoadFile(string sFileName, string remotePath);
        void UploadFolder(string sFolderPath);
        void UploadFolder(string localFolderPath, string remotePath);
        void UpLoadFile(string sFileName);
    }
}
