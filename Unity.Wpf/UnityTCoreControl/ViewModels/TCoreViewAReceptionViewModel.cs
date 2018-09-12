using EventObjects;
using EventObjects.EventObjects;
using EventPlatform;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UnityTCoreControl.ViewModels
{
    public class TCoreViewAReceptionViewModel : BindableBase
    {
        #region member data
        IEventAggregator _ea;
        IEventHandler _eh;
        IEventHandler _eh1;
        private string _title = "TCore Reception";
        private string _recvMsg;
        private string _recvMsg1;
        #endregion member data

        #region properties
        public string RecvMsg
        {
            get { return _recvMsg; }
            set { SetProperty(ref _recvMsg, value); }

        }
        public string RecvMsg1
        {
            get { return _recvMsg1; }
            set { SetProperty(ref _recvMsg1, value); }

        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion properties

        #region member function
        #region using EventHandler
        public TCoreViewAReceptionViewModel(IEventHandler eh, IEventHandler eh1)
        {
            _eh = eh;
            _eh1 = eh1;
            RecvMsg = "----";
            RecvMsg1 = "****";
            _eh.Subscribe(EventReceived);
            _eh1.Subscribe(EventReceived1);
        }
        private void EventReceived(object message)
        {
            if (message is string)
                RecvMsg = (string)message;
            else if(message is RequestEvent)
            {
                RequestEvent request = (RequestEvent)message;
                RecvMsg = (string)request.ReqObject;
                request.Reply("reply:" + DateTime.Now.ToString("HH:mm:ss"));
            }
        }
        private void EventReceived1(object message)
        {
            if (message is string)
                RecvMsg1 = (string)message;
        }
        #endregion //using EventHandler

        #region using EventAggregator
        public TCoreViewAReceptionViewModel(IEventAggregator ea)
        {
            _ea = ea;
            RecvMsg = "----";
            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }
        private void MessageReceived(object message)
        {
            if (message is string)
                RecvMsg = (string)message;
        }
        #endregion //using EventAggregator
        #endregion //member function
    }
}
