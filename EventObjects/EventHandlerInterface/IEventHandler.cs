using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects
{
    public interface IEventHandler
    {
        string HandlerName { get;}

        void Publish(object obj);

        void Subscribe(Action<object> action);

        void Unsubscribe(Action<object> action);

        bool Request(string key, object obj, Action<string, object> actionReply);
    }
}
