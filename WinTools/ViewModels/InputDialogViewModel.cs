using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WinTools.ViewModels
{
    public class InputDialogViewModel : INotifyPropertyChanged
    {
        private string _content;
        public event PropertyChangedEventHandler PropertyChanged;

        #region properties
        public ICommand OkCommand { get { return new DelegateCommand(OkClicked, CanOkClick); } }
        //public DelegateCommand<object> OkCommand
        //{
        //    get; private set;
        //}
        public string Content
        {
            get { return _content; }

            //set { SetProperty(ref _content, value); }
            set {
                _content = value;
                RaisePropertyChangedEvent("Content");
            }
        }
        #endregion //properties

        public InputDialogViewModel()
        {
            Content = "1234";
            //Message = "View A from your Prism Module";

            //OkCommand = new DelegateCommand(OkClicked);
        }
        private bool CanOkClick()
        {
            return true;
        }
        private void OkClicked(object inputDialg)
        {
            if(inputDialg is Window)
            {
                ((Window)inputDialg).DialogResult = true;
                ((Window)inputDialg).Close();
            }
            //inputDialg.Close();
        }

        protected void RaisePropertyChangedEvent(string propertyName = "")
        {
            //var handler = PropertyChanged;
            //if (handler != null)
            //    handler(this, new PropertyChangedEventArgs(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ~InputDialogViewModel()
        {
        }

    }
}
