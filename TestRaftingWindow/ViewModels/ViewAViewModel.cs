using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRaftingWindow.ViewModels
{
    class ViewAViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChangedEvent(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string _title = "ViewA";
        public string Title
        {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChangedEvent("Title");
            }
        }
    }
}
