using Akka.Actor;
using EventObjects;
using EventObjects.EventObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlatform
{
    //it suppose to create an akka system, and then it could create an actor.
    public class AkkaEventActor : ReceiveActor
    {
        //private ActorRef _actor;
        Action<object> _eventHandler = null;
        Action<object> _eventRefHandler = null;

        AkkaEventActorRef _eventHandlerRef = null;


        public IEventHandlerRef ReplyRef
        {
            get
            {
                if (_eventHandlerRef == null)
                    _eventHandlerRef = new AkkaEventActorRef(this.Self);
                return _eventHandlerRef;
            }
        }
        public void Request(string key, object obj, Action<string, object> actionReply)
        {
            //_eventRefHandler += new Action<object>((para) => {
            //    try
            //    {
            //        actionReply(para);
            //    }
            //    finally
            //    {
            //        _eventHandler -= actionReply;
            //    }
            //});

            //object result = null; //ReplyObj
            //                      //do something
            RequestEvent request = (RequestEvent)obj;
            ReplyEvent reply = new ReplyEvent() { ReplyObj = "123" };
            //reply.ReplyObj = result;
            request.Reply(reply);
        }

        public AkkaEventActor()
        {
            Receive<object>(obj =>
            {
                if(obj is AkkaUnsub)
                {
                    AkkaUnsub unsubObj = (AkkaUnsub)obj;
                    _eventHandler -= unsubObj.SubFunc;
                }
                else if(obj is AkkaSub)
                {
                    AkkaSub subObj = (AkkaSub)obj;
                    _eventHandler += subObj.SubFunc;
                }
                else if (obj is AkkaRequest)
                {
                    AkkaRequest reqObj = (AkkaRequest)obj;
                    Request(reqObj.Key, reqObj.ReqObject, reqObj.ReplyFunc);
                }
                else if (_eventRefHandler != null && obj is ReplyEvent)
                    _eventRefHandler(obj); //call to reply function
                else if (_eventHandler != null)
                    _eventHandler(obj); //call to subscribed function

            });
        }
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
            Action<string, object> _replyFunc;
            object _reqObj;
            string _key;


            public string Key
            {
                get { return _key; }
                set { _key = value; }
            }
            public Action<string, object> ReplyFunc
            {
                get { return _replyFunc; }
                set { _replyFunc = value; }
            }

            public object ReqObject
            {
                get { return _reqObj; }
                set { _reqObj = value; }
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
    }
}
