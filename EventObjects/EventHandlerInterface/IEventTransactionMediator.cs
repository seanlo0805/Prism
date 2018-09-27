using System;

namespace EventObjects.EventHandlerInterface
{

    public interface IEventTransactionMediator
    {
        bool CreatePrivateChannelSync(string channel, Action<object> OnRequestCall);
        bool RequestSync(string channel, object req, Action<object> OnReplyCall);

        bool RegisterRequestAsync(string channel, Action<object> OnRequestCall);
        bool RegisterReplyAsync(string channel, Action<object> OnReplyCall);
        bool RequestAsync(string channel, object request);
        bool ReplyAsync(string channel, object reply);
    }
}
