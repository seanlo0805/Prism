using EventObjects;
using EventObjects.EventObjects;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlatform
{
    /// <summary>
    /// (publisher) -------publush(_eventAggregator) ----> (subscriber)
    /// (requester) -------request(_eventAggregator) ----> (replier)
    ///                                                       |
    ///             o------reply(_eventAggregatorReply) ------+
    /// </summary>
    public class PrismEventAggregator : IEventHandler, IEventHandlerRef
    {
        IEventAggregator _eventAggregator;

        private string _handlerName;

        //for pvivate channel(transactions)
        IEventAggregator _eventAggregatorReply;

        /// <summary>
        /// 用來處理reply工作function的sub / unsbu, 必免一直累積
        /// 最後還是透過它來呼叫replyCallback
        /// </summary>
        Dictionary<string, Action<object>> _replyActions = new Dictionary<string, Action<object>>();

        private IEventAggregator ReplyRef
        {
            get {
                if (_eventAggregatorReply == null)
                    _eventAggregatorReply = new EventAggregator();
                return _eventAggregatorReply;
            }
        }

        public string HandlerName
        {
            get { return _handlerName; }
        }

        public PrismEventAggregator(string handlerName)
        {
            _handlerName = handlerName;
            _eventAggregator = new EventAggregator();
        }

        public void Publish(object obj)
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(obj);
        }


        public void Subscribe(Action<object> action)
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(action);
        }
        public void Unsubscribe(Action<object> action)
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Unsubscribe(action);
        }

        #region for Transactions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origReq">original request object</param>
        /// <param name="obj">ReplyEvent</param>
        public void Reply(RequestEvent origReq, object obj)
        {
            ReplyRef.GetEvent<MessageSentEvent>().Publish(new TransactionObject(origReq.Key, obj, origReq.ReplyCallback));
        }
        public bool Request(string key, object obj, Action<string, object> actionCallback)
        {
            if (_replyActions.ContainsKey(key))
                return false;
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
                        if (_replyActions.ContainsKey(key))
                            ReplyRef.GetEvent<MessageSentEvent>().Unsubscribe(_replyActions[key]);
                        //_eventAggregatorReply = null;
                    }
                }
            });
            _replyActions.Add(key, replyAction);
            //設定Reply回來時的func
            ReplyRef.GetEvent<MessageSentEvent>().Subscribe(replyAction);

            //在發出Request時, 就指定好Reply物件, 並在裡面也己經包含了ReplyFunc, 見property
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(new RequestEvent(key, this, actionCallback, obj));

            return true;
        }

        public class TransactionObject
        {
            /// <summary>
            /// ReplyEvent
            /// </summary>
            public object Para {get;set;}
            /// <summary>
            /// replyCallback
            /// </summary>
            public Action<string, object> ActionFunc { get; set; }
            /// <summary>
            /// original Request Key
            /// </summary>
            public string RequestKey { get; set; }
            public TransactionObject(string reqKey, object para, Action<string, object> actionFunc)
            {
                RequestKey = reqKey;
                Para = para;
                ActionFunc = actionFunc;
            }
        }
        #endregion // for Transactions
    }
}
