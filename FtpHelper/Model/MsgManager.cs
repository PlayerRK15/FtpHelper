using System;
using System.ComponentModel;
namespace FtpHelper.Model
{
    public class MsgManager : INotifyPropertyChanged
    {
        private string _msg;
        public string Msg { get => _msg; set { _msg = value; OnPropertyChanged("Msg"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string properName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properName));
        }
        public void AddMsg(string msg)
        {
            Msg += msg + "\r\n";
        }
    }
}
