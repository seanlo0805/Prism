using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects.EventObjects
{
    public  class TextEvent : EventBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public TextEvent() : base(EventType.Text)
        {
        }
    }
}
