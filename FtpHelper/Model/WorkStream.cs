using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpHelper.Model
{
    public class WorkStream : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string[] Props { get; set; }
    }
}
