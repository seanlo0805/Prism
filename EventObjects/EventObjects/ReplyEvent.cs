using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects.EventObjects
{
    public class ReplyEvent : EventBase
    {
        private object _replyObj;

        public object ReplyObj
        {
            get { return _replyObj; }
            set { _replyObj = value; }
        }
        public ReplyEvent() : base(EventType.Reply)
        {
        }

    }
}
