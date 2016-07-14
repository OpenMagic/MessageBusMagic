using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBusMagic.Support;

namespace MessageBusMagic
{
    public class MessageBus : IMessageBus
    {
        private readonly MessageHandlers MessageHandlers;

        public MessageBus()
        {
            MessageHandlers = new MessageHandlers();
        }

        public void SubscribeTo<TMessage>(Func<TMessage, Task> handler) where TMessage : IMessage
        {
            MessageHandlers.AddHandler(handler);
        }

        public IEnumerable<Task> Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            var handlers = MessageHandlers.GetHandlers<TMessage>();

            return from handler in handlers
                   select handler(message);
        }
    }
}