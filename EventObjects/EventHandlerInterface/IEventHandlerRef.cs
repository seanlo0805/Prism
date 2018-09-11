using EventObjects.EventObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventObjects
{
    public interface IEventHandlerRef
    {

        void Reply(RequestEvent origReq, object obj);

        //Action<object> ReplyFunc { get;}
    }
}
