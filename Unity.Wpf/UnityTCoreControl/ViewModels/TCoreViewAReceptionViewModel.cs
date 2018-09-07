using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityTCoreControl.ViewModels
{
    public class TCoreViewAReceptionViewModel : BindableBase
    {
        IEventAggregator _ea;
        private string _title = "This is TCore Reception";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public TCoreViewAReceptionViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }
    }
}
