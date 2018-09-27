using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects.EventHandlerInterface
{
    public interface IServiceProviderHandler
    {
        //create and timeout jedgement?
        bool CreateService(string service, Type actorType, object[] args);
        bool CreateService(string service, Type actorType);
        bool StartService(string service);
        IServiceProviderHandler GetService(string service);
    }
}
