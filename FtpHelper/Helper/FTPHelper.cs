using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FtpHelper.Helper
{
    public static class FTPHelper
    {

        public static string UpLoadFile(string user, string password, FileInfo filePath, string ftpUrl, string fileToFTPName)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl + fileToFTPName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(user, password);
                using (FileStream fileStream = File.Open(filePath.FullName, FileMode.Open, FileAccess.Read))
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyTo(requestStream, 1024);
                    }
                    TimeSpan EndTime = DateTime.Now - StartTime;
                    return $"文件:{filePath}上传完成" + $"耗时:{EndTime.TotalSeconds}秒,文件大小:{filePath.Length / 1024}KB";
                }
            }
            catch (Exception e)
            {
                return $"文件:{filePath}上传失败,错误详情:{e.Message}";
            }
        }
        public static string CreateFtpDir(string user, string password, string DirName, string ftpUrl, string DirParent)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                string uri = $@"{ftpUrl}\{DirParent}\{DirName}";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Credentials = new NetworkCredential(user, password);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse Reson = (FtpWebResponse)request.GetResponse();
                Reson.Close();
                TimeSpan EndTime = DateTime.Now - StartTime;
                return $"文件夹:{DirName}创建完成" + $"耗时:{EndTime.TotalSeconds}秒";
            }
            catch (Exception e)
            {
                return $"文件夹:{DirName}创建失败,错误详情:{e.Message}";
            }

        }
        /// <summary>
        /// 新路径必须是相对路径
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="ftpFilePath"></param>
        /// <param name="newFtpFilePath">需要相对路径</param>
        /// <returns></returns>
        public static string MoveFtpFile(string user, string password, string ftpFilePath, string newFtpFilePath)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                string uri = ftpFilePath;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Credentials = new NetworkCredential(user, password);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.RenameTo = newFtpFilePath;
                FtpWebResponse Reson = (FtpWebResponse)request.GetResponse();
                Reson.Close();
                TimeSpan EndTime = DateTime.Now - StartTime;
                return $"文件:{ftpFilePath}移动完成" + $"耗时:{EndTime.TotalSeconds}秒";
            }
            catch (Exception e)
            {
                return $"文件:{ftpFilePath}移动失败,错误详情:{e.Message}";
            }
        }
        public static string[] FtpGetDirs(string user, string password, string dirUri)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUri);
                request.Credentials = new NetworkCredential(user, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                var Reson = new StreamReader(request.GetResponse().GetResponseStream());
                var str = Reson.ReadToEnd().Split('\r');
                Reson.Close();
                TimeSpan EndTime = DateTime.Now - StartTime;
                return str;
            }
            catch (Exception e)
            {
                return new string[] { e.Message };
            }
        }
        public static string FtpDeleteFile(string user, string password, string ftpFilePath)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                string uri = ftpFilePath;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Credentials = new NetworkCredential(user, password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse Reson = (FtpWebResponse)request.GetResponse();
                Reson.Close();
                TimeSpan EndTime = DateTime.Now - StartTime;
                return $"文件:{ftpFilePath}删除完成" + $"耗时:{EndTime.TotalSeconds}秒";
            }
            catch (Exception e)
            {
                return $"文件:{ftpFilePath}删除失败,错误详情:{e.Message}";
            }
        }
        public static string FtpDeleteDir(string user, string password, string ftpFilePath)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                string uri = ftpFilePath;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Credentials = new NetworkCredential(user, password);
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse Reson = (FtpWebResponse)request.GetResponse();
                Reson.Close();
                TimeSpan EndTime = DateTime.Now - StartTime;
                return $"文件夹:{ftpFilePath}删除完成" + $"耗时:{EndTime.TotalSeconds}秒";
            }
            catch (Exception e)
            {
                return $"文件夹:{ftpFilePath}删除失败,错误详情:{e.Message}";
            }
        }
        public static string GetSubString(this string[] strs, string enter = "\r\n")
        {
            string str = "";
            foreach (var item in strs)
            {
                str += item + enter;
            }
            return str;
        }
    }
    public class FTPDirModel
    {
        private FtpWebRequest _request;
        public string Path { get; set; }
        public List<FTPDirModel> Chileds { get; set; }
        public List<string> ChiledFiles { get; set; }
        public FTPDirModel(string path, string user, string password)
        {
            Path = path;
            _request = (FtpWebRequest)WebRequest.Create(path);
            _request.Credentials = new NetworkCredential(user, password);
        }
        public string Upload()
        {
            _request.Method = WebRequestMethods.Ftp.UploadFile;
            return "";
        }
    }
}
