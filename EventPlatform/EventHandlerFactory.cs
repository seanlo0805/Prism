using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlatform
{
    //define generic T class with default contructor inhirent from IEventHandler
    public class EventHandlerFactory<T> where T : IEventHandler, new()
    {
        static Dictionary<string, IEventHandler> _mapEventHandlers = new Dictionary<string, IEventHandler>();
        public static IEventHandler FetchEventHandler(string channel)
        {
            if (!_mapEventHandlers.ContainsKey(channel))
            {
                IEventHandler eventHandler =  new T();
                _mapEventHandlers.Add(channel, eventHandler);
            }
            return _mapEventHandlers[channel];
        }

    }
}
