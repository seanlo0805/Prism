﻿using Akka.Actor;
using EventObjects;
using EventObjects.EventObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EventPlatform.AkkaEventActor;
using static EventPlatform.PrismEventAggregator;

namespace EventPlatform
{
    public class AkkaEventHandler : IEventHandler, IEventHandlerRef
    {
        private static ActorSystem _akkaSystem_ = ActorSystem.Create("PrismEventPlatform");
        /// <summary>
        /// 用來處理reply工作function的sub / unsbu, 必免一直累積
        /// 最後還是透過它來呼叫replyCallback
        /// </summary>
        Dictionary<string, ActorMeta> _actorPool_ = new Dictionary<string, ActorMeta>();

        private IActorRef _actorRef;
        private string _handlerName;

        AkkaEventActorRef _eventHandlerRef = null;
        Action<object> _replyFunc;


        public IEventHandlerRef ReplyRef
        {
            get
            {
                if (_eventHandlerRef == null)
                    _eventHandlerRef = new AkkaEventActorRef(_actorRef);
                return _eventHandlerRef;
            }
        }

        public string HandlerName {
            get { return _handlerName; }
        }


        public AkkaEventHandler(string handlerName) {
            _handlerName = handlerName;
            lock (_actorPool_)
            {
                if (!_actorPool_.ContainsKey(handlerName))
                {
                    _actorRef = _akkaSystem_.ActorOf(Props.Create(() => new AkkaEventActor()), _handlerName);
                    _actorPool_.Add(handlerName, new ActorMeta() { Count = 1, ActorRef = _actorRef });
                }
                else
                {
                    _actorRef = _actorPool_[handlerName].ActorRef;
                    _actorPool_[handlerName].Count++;
                }
            }
            //for pvivate channel(transactions)
            _replyFunc += new Action<object>((obj) => {
                if (obj is TransactionObject)
                {
                    TransactionObject transactionMeta = (TransactionObject)obj;
                    try
                    {
                        transactionMeta.ActionFunc(transactionMeta.RequestKey, transactionMeta.Para);
                    }
                    finally
                    {
                        //ReplyRef.GetEvent<MessageSentEvent>().Unsubscribe(_replyFunc);
                        //_eventAggregatorReply = null;
                    }
                }
            });
        }
        ~AkkaEventHandler()
        {
            lock (_actorPool_)
            {
                _actorPool_[_handlerName].Count--;
                if (_actorPool_[_handlerName].Count <= 0)
                    _actorPool_.Remove(_handlerName);
            }
        }
        public void Publish(object obj)
        {
            _actorRef?.Tell(obj);
        }

        public void Subscribe(Action<object> action)
        {
            _actorRef?.Tell(new AkkaSub() { SubFunc = action });
        }

        public void Unsubscribe(Action<object> action)
        {
            _actorRef?.Tell(new AkkaUnsub() { SubFunc = action });
        }
        #region for Transactions

        public bool Request(string key, object obj, Action<string, object> actionCallback)
        {
            //_actorRef?.Tell(new AkkaRequest() { Key = key, ReqObject = obj, ActionCallBack = actionCallback });
            _actorRef?.Tell(new AkkaRequest() { RequestEvent = new RequestEvent(key, this, actionCallback, obj) });
            return true;
        }
        public void Reply(RequestEvent origReq, object obj)
        {
            _actorRef?.Tell(new TransactionObject(origReq.Key, obj, origReq.ReplyCallback));
        }


        #endregion // for Transactions

        public class ActorMeta{
            public int Count { get; set; }
            public IActorRef ActorRef { get; set; }
        }
    }
}
