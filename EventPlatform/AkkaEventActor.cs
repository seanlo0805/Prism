using Akka.Actor;
using EventObjects;
using EventObjects.EventObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static EventPlatform.PrismEventAggregator;

namespace EventPlatform
{
    /// <summary>
    /// Akka當作Event Handler的優點在於, 自帶的Actor特性, 可以說默認多工的Handler
    /// Akka本身應該可以適用更多元的EventMessager, 讓每個畫面都持有一個Actor來使用
    /// 這裡把一個Actor也當成一個EventAggregator, 受侷限了
    /// </summary>
    public class AkkaEventActor : ReceiveActor
    {
        #region member data
        //private ActorRef _actor;
        Action<object> _eventAction = null;
        Action<object> _eventReplyAction = null;

        AkkaEventActorRef _eventHandlerRef = null;

        /// <summary>
        /// 用來處理reply工作function的sub / unsbu, 避免一直累積
        /// 最後還是透過它來呼叫replyCallback
        /// </summary>
        Dictionary<string, Action<object>> _replyActions = new Dictionary<string, Action<object>>();

        #endregion // member data

        #region properties
        public IEventHandlerRef ReplyRef
        {
            get
            {
                if (_eventHandlerRef == null)
                    _eventHandlerRef = new AkkaEventActorRef(this.Self);
                return _eventHandlerRef;
            }
        }

        #endregion //properties

        #region member function
        public AkkaEventActor()
        {
            Receive<object>(obj =>
            {
                try
                {
                    if (obj is AkkaUnsub)
                    {
                        AkkaUnsub unsubObj = (AkkaUnsub)obj;
                        _eventAction -= unsubObj.SubFunc;
                    }
                    else if (obj is AkkaSub)
                    {
                        AkkaSub subObj = (AkkaSub)obj;
                        _eventAction += subObj.SubFunc;
                    }
                    else if (_eventAction != null && obj is AkkaRequest)
                    {
                        AkkaRequest reqObj = (AkkaRequest)obj;
                        Request((AkkaRequest)obj);
                    }
                    else if (_eventReplyAction != null && obj is TransactionObject)
                    {
                        _eventReplyAction(obj); //call to reply function
                    }
                    else if (_eventAction != null)
                        _eventAction(obj); //call to subscribed function for publish
                }catch(Exception e)
                {
                    //log.error
                    //System.Windows.Forms.MessageBox.Show(e.ToString(), "Actor Excpetion", MessageBoxButtons.OK);
                }

            });
        }
        protected void Request(AkkaRequest reqObj)
        {

            if (_replyActions.ContainsKey(reqObj.RequestEvent.Key))
                return;   //duplicated request

            Action<object> replyAction = new Action<object>((transObj) => {
                if (transObj is TransactionObject)
                {
                    TransactionObject transactionMeta = (TransactionObject)transObj;
                    try
                    {
                        transactionMeta.ActionFunc(transactionMeta.RequestKey, transactionMeta.Para); // call replyCallback
                    }
                    finally
                    {
                        if (_replyActions.ContainsKey(reqObj.RequestEvent.Key))
                            _eventReplyAction -= _replyActions[reqObj.RequestEvent.Key];
                    }
                }
            });
            _replyActions.Add(reqObj.RequestEvent.Key, replyAction);

            _eventReplyAction += replyAction;

            if (_eventAction != null)
                _eventAction(reqObj.RequestEvent);  //call to subscribed function for request
        }
        #endregion //member function

        #region akka event class
        public class AkkaSub
        {
            Action<object> _subFunc;
            public Action<object> SubFunc
            {
                get { return _subFunc; }
                set { _subFunc = value; }
            }

        }
        public class AkkaRequest
        {
            RequestEvent _requsetEvent;


            public RequestEvent RequestEvent
            {
                get { return _requsetEvent; }
                set { _requsetEvent = value; }
            }
        }
        public class AkkaReply
        {
            Action<object> _replyFunc;
            object _replyObj;

            public Action<object> ReplyFunc
            {
                get { return _replyFunc; }
                set { _replyFunc = value; }
            }

            public object ReplyObject
            {
                get { return _replyObj; }
                set { _replyObj = value; }
            }
        }
        public class AkkaUnsub
        {
            Action<object> _subFunc;
            public Action<object> SubFunc
            {
                get { return _subFunc; }
                set { _subFunc = value; }
            }

        }
        #endregion //akka event class
    }
}
