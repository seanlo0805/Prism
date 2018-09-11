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
    public class AkkaEventActorRef : IEventHandlerRef
    {
        public Action<object> ReplyFunc => throw new NotImplementedException();

        IActorRef _actorRef;
        public AkkaEventActorRef(IActorRef actorRef)
        {
            _actorRef = actorRef;
        }


        public void Reply(RequestEvent origReq, object obj)
        {
            _actorRef?.Tell(obj);
        }
    }
}
