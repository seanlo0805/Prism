using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityAutowireControl.ViewModels
{
    class ViewAReceptionViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        //you could get IEventAggregator (Dependency Inject) only when you implement ViewModel Autowiring otherwise you should pass ea youself(See UnitTCoreControl and UnityReceptControl)
        public ViewAReceptionViewModel(IEventAggregator ea)
        {
            Message = "This is ViewAReception";
        }
    }
}
