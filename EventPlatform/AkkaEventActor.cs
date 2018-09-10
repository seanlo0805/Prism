using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlatform
{
    //it suppose to create an akka system, and then it could create an actor.
    public class AkkaEventActor : IEventHandler//, ReceiveActor
    {
        //private ActorRef _actor;
        Action<object> _eventHandler = null;

        public AkkaEventActor()
        {
            //Receive<object>(obj =>
            //{
            //    if (eventHandler != null)
            //        eventHandler(obj);

            //});
        }
        public void Publish(object obj)
        {
            //_actor?.tell(object);
        }

        public void Subscribe(Action<object> action)
        {
            _eventHandler += action;
        }

        public void Unsubscribe(Action<object> action)
        {
            _eventHandler -= null;
        }
    }
}
