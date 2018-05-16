using System;
namespace FlieFTP
{
    public interface IEACTFTP
    {
        void _UploadFolder(string localFolderPath, string remotePath);
        void CloseFTP();
        void Delete(string fileName);
        void DeleteFtpDirWithAll(string sFolderName, bool isDeleteFolder = true);
        bool DirectoryExist(string LocaPath, string RemoteDirectoryName);
        bool DirectoryExist(string RemoteDirectoryName);
        void DownloadFile(string localPath, string remotePath, string remoteFile);
        void DownloadFile(string sFilePath, string sFileName);
        void DownloadFolder(string sFolderPath, string remotePath, string sFolderName);
        void DownloadFolder(string sFolderPath, string sFolderName);
        bool FileExist(string LocaPath, string RemoteFileName);
        bool FileExist(string RemoteFileName);
        string FtpURI { get; }
        string[] GetDirectoryList();
        string[] GetDirectoryList(string LocaPath);
        string[] GetFileList();
        string[] GetFileList(string LocaPath);
        string[] GetFilesDetailList();
        long GetFileSize(string filename);
        void MakeDir(string dirName);
        void MakeDirPath(string DirPath);
        void NextDirectory(string sFolderName);
        void RemoveDirectory(string folderName);
        void ReName(string currentFilename, string newFilename);
        void UpDirectory();
        void UpLoadFile(string sFileName);
        void UpLoadFile(string sFileName, string remotePath);
        void UploadFolder(string localFolderPath, string remotePath);
        void UploadFolder(string sFolderPath);
    }
}
