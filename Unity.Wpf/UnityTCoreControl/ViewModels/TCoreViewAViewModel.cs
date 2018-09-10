using EventObjects;
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
        private string _title = "TCore ViewA";
        #endregion member data

        #region properties
        public DelegateCommand SendMessageCommand
        {
            get; private set;
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion properties

        #region member function
        #region using Event Handler
        public TCoreViewAViewModel(IEventHandler eh)
        {
            _eh = eh;
            SendMessageCommand = new DelegateCommand(SendEvent);
        }
        private void SendEvent()
        {
            _eh.Publish(DateTime.Now.ToString("HH:mm:ss") + " => [from] " + _title);
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
