using EventObjects;
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
        #region member data
        IEventAggregator _ea;
        private string _recvMsg;
        private string _message;
        #endregion // member data

        #region properties
        public string RecvMsg
        {
            get { return _recvMsg; }
            set { SetProperty(ref _recvMsg, value); }

        }
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion //properties

        #region member function
        //you could get IEventAggregator (Dependency Inject) only when you implement ViewModel Autowiring otherwise you should pass ea youself(See UnitTCoreControl and UnityReceptControl)
        public ViewAReceptionViewModel(IEventAggregator ea)
        {
            _ea = ea;
            Message = "This is ViewAReception";
            RecvMsg = "----";

            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }
        private void MessageReceived(object message)
        {
            if(message is string)
                RecvMsg = (string)message;
        }
        #endregion // member function
    }
}
