using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.Utilities.Services.Messaging
{
    public interface IAsyncMessagePublisher
    {
        void Publish<TInput>(TInput input);
        void Publish(string jsonInput);
        void Publish(string messageId, string correlationId,string eventName, string jsonInput);

        void Publish(string rout,string jsonInput);
    }

}
