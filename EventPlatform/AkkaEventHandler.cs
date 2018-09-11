using Akka.Actor;
using EventObjects;
using EventObjects.EventObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EventPlatform.AkkaEventActor;
using static EventPlatform.PrismEventAggregator;

namespace EventPlatform
{
    public class AkkaEventHandler : IEventHandler, IEventHandlerRef
    {
        private static ActorSystem _akkaSystem_ = ActorSystem.Create("PrismEventPlatform");
        private IActorRef _actorRef;
        private string _handlerName;

        Action<object> _eventHandler = null;
        Action<object> _eventRefHandler = null;

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


        public AkkaEventHandler(string handlerName){
            _handlerName = handlerName;
            _actorRef = _akkaSystem_.ActorOf(Props.Create(() => new AkkaEventActor()), _handlerName);

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

        public bool Request(string key, object obj, Action<string, object> actionReply)
        {
            _actorRef?.Tell(new AkkaRequest() { Key= key, ReqObject = obj, ReplyFunc = actionReply });
            return true;
        }
        public void Reply(RequestEvent origReq, object obj)
        {
            _actorRef?.Tell(obj);
        }
        #endregion // for Transactions


    }
}
