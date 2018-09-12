using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects.EventObjects
{
    public class RequestEvent : EventBase
    {
        private string _key;
        private object _reqObject;
        private IEventHandlerRef _replyHandler;
        private Action<string, object> _replyCallback;

        public string Key
        {
            get { return _key; }
        }
        public object ReqObject
        {
            get { return _reqObject; }
            set { _reqObject = value; }
        }
        public Action<string, object> ReplyCallback
        {
            get { return _replyCallback; }
        }
        public IEventHandlerRef ReplyHandler
        {
            get { return _replyHandler; }
        }
        public RequestEvent(string key, IEventHandlerRef replyHandler, Action<string, object> replyCallback, object reqObj) : base(EventType.Request)
        {
            _key = key;
            _replyHandler = replyHandler;
            _replyCallback = replyCallback;
            _reqObject = reqObj;
        }

        /// <summary>
        /// 處理request結束後, 透過reply回應給原要求端
        /// </summary>
        /// <param name="reply"></param>
        public void Reply(object reply)
        {
            ReplyEvent replyEvent = new ReplyEvent();
            replyEvent.ReplyObj = reply;

            //因為request內包含原始委託來源, 要透過這項資訊回送
            ReplyHandler.Reply(this, replyEvent);
        }
    }
}
