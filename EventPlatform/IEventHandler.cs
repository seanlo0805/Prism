using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlatform
{
    public interface IEventHandler
    {
        
        void Publish(object obj);

        void Subscribe(Action<object> action);

        void Unsubscribe(Action<object> action);
    }
}
