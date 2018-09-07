using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFTCoreControl.ViewModel
{
    [Export(typeof(TCoreViewModelA))]
    class TCoreViewModelA : BindableBase
    {
        private string _title = "This is TCoreViewModelA";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
