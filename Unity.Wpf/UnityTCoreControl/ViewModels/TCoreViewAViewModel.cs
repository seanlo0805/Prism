using EventObjects;
using EventObjects.EventObjects;
using EventPlatform;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityTCoreControl.ViewModels
{
    /// <summary>
    /// 
    /// reference: https://dotblogs.com.tw/keigen/2017/07/17/091302
    ///            https://vulcanlee.gitbooks.io/xamarin-forms-develop-notes/GroupNotes5/ViewCoding.html
    /// </summary>
    public class TCoreViewAViewModel : BindableBase
    {
        #region member data
        IEventAggregator _ea;
        IEventHandler _eh;
        IEventHandler _eh1;
        private string _title = "TCore ViewA";
        private string _replyMsg = "replyMsg";
        #endregion member data

        #region properties
        public DelegateCommand SendMessageCommand
        {
            get; private set;
        }
        public DelegateCommand SendMessageCommand1
        {
            get; private set;
        }
        public string ReplyMsg
        {
            get { return _replyMsg; }
            set { SetProperty(ref _replyMsg, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion properties

        #region member function
        #region using Event Handler
        public TCoreViewAViewModel(IEventHandler eh, IEventHandler eh1)
        {
            _eh = eh;
            _eh1 = eh1;
            SendMessageCommand = new DelegateCommand(SendEvent);
            SendMessageCommand1 = new DelegateCommand(SendEvent1);
        }
        private void SendEvent()
        {
            _eh.Publish(DateTime.Now.ToString("HH:mm:ss") + " => [from #0] " + _title);
            _eh.Request(Guid.NewGuid().ToString(), DateTime.Now.ToString("HH:mm:ss") + " => [request #0] " + _title, RecieveReply);
        }
        private void RecieveReply(string reqKey, object obj)
        {
            if(obj is ReplyEvent)
            {
                ReplyEvent reply = (ReplyEvent)obj;
                ReplyMsg = reply.ReplyObj.ToString();
            }
        }
        private void SendEvent1()
        {
            _eh1.Publish(DateTime.Now.ToString("HH:mm:ss") + " => [from #1] " + _title);
        }
        #endregion //using Event Handler

        #region using EventAggregator
        public TCoreViewAViewModel(IEventAggregator ea)
        {
            _ea = ea;
            SendMessageCommand = new DelegateCommand(SendMessage);
        }
        private void SendMessage()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(DateTime.Now.ToString("HH:mm:ss") + " => [from] " + _title);
        }
        #endregion //using EventAggregator
        #endregion //member function
    }
}
