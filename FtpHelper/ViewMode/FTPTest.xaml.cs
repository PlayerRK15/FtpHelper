using System;
using FtpHelper.Model;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using FtpHelper.Helper;

namespace FtpHelper.ViewMode
{
    /// <summary>
    /// FTPTest.xaml 的交互逻辑
    /// </summary>
    public partial class FTPTest : UserControl
    {
        public FTPTest()
        {
            InitializeComponent();
            Msgs = new MsgManager();
            txtBox.DataContext = Msgs;
        }
        public MsgManager Msgs { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.ShowDialog();
            var user = FtpUserName.Text;
            var password = FtpPassword.Text;
            var uri = FtpUri.Text;
            var filePath = new FileInfo(dialog.FileName);
            new Thread(() =>
            {
                lock (Msgs)
                {
                    Msgs.AddMsg(FTPHelper.UpLoadFile(user, password, filePath, uri, filePath.Name));
                }
            }).Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dirName = DirName.Text;
            var user = FtpUserName.Text;
            var password = FtpPassword.Text;
            var uri = FtpUri.Text;
            new Thread(() =>
            {
                lock (Msgs)
                {
                    Msgs.AddMsg(FTPHelper.CreateFtpDir(user, password, dirName, uri, ""));
                }
            }).Start();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var filePath = ftpFilePath.Text;
            var newFilePath = newftpFilePath.Text;
            var user = FtpUserName.Text;
            var password = FtpPassword.Text;
            new Thread(() =>
            {
                lock (Msgs)
                {
                    Msgs.AddMsg(FTPHelper.MoveFtpFile(user, password, filePath, newFilePath));
                }
            }).Start();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var path = dirPath.Text;
            var user = FtpUserName.Text;
            var password = FtpPassword.Text;
            var uri = FtpUri.Text;
            new Thread(() =>
            {
                lock (Msgs)
                {
                    Msgs.AddMsg(FTPHelper.FtpGetDirs(user, password, path).GetSubString(""));
                }
            }).Start();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var path = deleteFile.Text;
            var user = FtpUserName.Text;
            var password = FtpPassword.Text;
            var uri = FtpUri.Text;
            new Thread(() =>
            {
                lock (Msgs)
                {
                    Msgs.AddMsg(FTPHelper.FtpDeleteFile(user, password, path));
                }
            }).Start();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var path = deleteDir.Text;
            var user = FtpUserName.Text;
            var password = FtpPassword.Text;
            var uri = FtpUri.Text;
            new Thread(() =>
            {
                lock (Msgs)
                {
                    Msgs.AddMsg(FTPHelper.FtpDeleteDir(user, password, path));
                }
            }).Start();
        }
    }

}
