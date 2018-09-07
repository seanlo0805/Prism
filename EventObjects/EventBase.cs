using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects
{
    abstract public class EventBase 
    {
        private EventType _eventType;

        public EventType EventType
        {
            get { return _eventType; }
        }

        public EventBase(EventType type)
        {
            _eventType = type;
        }
    }
}
