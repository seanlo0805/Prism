using EventObjects;
using EventObjects.EventObjects;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EventPlatform.PrismEventAggregator;

namespace EventPlatform
{
    public class PrismEventAggregatorAsync : IEventHandler, IEventHandlerRef
    {
        //https://stackoverflow.com/questions/2834035/eventaggregator-is-it-thread-safe
        //Prsim EventAggregator is not Thread-Safe, lock it

        IEventAggregator _eventAggregator;

        private string _handlerName;

        //for pvivate channel(transactions)
        IEventAggregator _eventAggregatorReply;
        object replyLocker = new object();

        /// <summary>
        /// 用來處理reply工作function的sub / unsbu, 必免一直累積
        /// 最後還是透過它來呼叫replyCallback
        /// </summary>
        Dictionary<string, Action<object>> _replyActions = new Dictionary<string, Action<object>>();

        private IEventAggregator ReplyRef
        {
            get
            {
                if (_eventAggregatorReply == null)
                    _eventAggregatorReply = new EventAggregator();
                return _eventAggregatorReply;
            }
        }

        public string HandlerName
        {
            get { return _handlerName; }
        }


        public PrismEventAggregatorAsync(string handlerName)
        {
            _handlerName = handlerName;
            _eventAggregator = new EventAggregator();
        }

        public void Publish(object obj)
        {
            Task.Run(() => {
                lock (_eventAggregator)
                {
                    _eventAggregator.GetEvent<MessageSentEvent>().Publish(obj);
                }
            });

        }

        public void Subscribe(Action<object> action)
        {
            lock (_eventAggregator)
            {
                _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(action);
            }
        }
        public void Unsubscribe(Action<object> action)
        {
            lock (_eventAggregator)
            {
                _eventAggregator.GetEvent<MessageSentEvent>().Unsubscribe(action);
            }
        }


        #region for Transactions
        public void Reply(RequestEvent origReq, object obj)
        {
            //Task.Run(() =>
            //{
                lock (replyLocker)
                {
                    ReplyRef.GetEvent<MessageSentEvent>().Publish(new TransactionObject(origReq.Key, obj, origReq.ReplyCallback));
                }
            //});
        }

        public bool Request(string key, object obj, Action<string, object> actionCallBack)
        {
            lock (replyLocker)
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
                            lock (replyLocker)
                            {
                                if (_replyActions.ContainsKey(key))
                                    ReplyRef.GetEvent<MessageSentEvent>().Unsubscribe(_replyActions[key]);
                            }
                            //_eventAggregatorReply = null;
                        }
                    }
                });
                _replyActions.Add(key, replyAction);
                //設定Reply回來時的func
                ReplyRef.GetEvent<MessageSentEvent>().Subscribe(replyAction);
            }

            lock (_eventAggregator)
            {
                //在發出Request時, 就指定好Reply物件this(IEventHandlerRef), 並在裡面也己經包含了ReplyRef, 見property
                _eventAggregator.GetEvent<MessageSentEvent>().Publish(new RequestEvent(key, this, actionCallBack, obj));

            }
            return true;
        }

        #endregion // for Transactions
    }
}
