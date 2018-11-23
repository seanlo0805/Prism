using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestTriggers.ViewModels
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public MainWindowModel()
        {
            Clicked = new RelayCommand(this.OnClick);
        }

        public ICommand Clicked { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnClick(object arg)
        {
            //为了演示需要，在这里用了一个MessageBox
            //应尽量避免在VM中揉杂UI交互功能
            MessageBox.Show("");
        }
    }
}
