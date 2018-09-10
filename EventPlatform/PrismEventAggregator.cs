using EventObjects;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlatform
{
    public class PrismEventAggregator : IEventHandler
    {
        IEventAggregator _eventAggregator;
        public PrismEventAggregator()
        {
            _eventAggregator = new EventAggregator();
        }
        public void Publish(object obj)
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(obj);
        }

        public void Subscribe(Action<object> action)
        {
            //_eventAggregator.GetEvent<MessageSentEvent>().Subscribe((obj) => action(obj));
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(action);
        }
        public void Unsubscribe(Action<object> action)
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Unsubscribe(action);
        }
    }
}
