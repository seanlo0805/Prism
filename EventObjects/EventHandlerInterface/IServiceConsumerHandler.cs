using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects.EventHandlerInterface
{
    public interface IServiceConsumerHandler
    {
        //add timeout call?
        bool SubscribeService(string service, Action<object> OnServiceCall);
    }
}
