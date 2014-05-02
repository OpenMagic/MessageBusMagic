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

        public Task SubscribeTo<TMessage>(Func<IMessage, Task> handler) where TMessage : IMessage
        {
            // todo: Test IMessage in Func<IMessage, Task> is TMessage.
            return Task.Factory.StartNew(() => MessageHandlers.SubscribeTo<TMessage>(handler));
        }

        public IEnumerable<Task> Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            var handlers = MessageHandlers.FindHandlersFor<TMessage>();

            return from handler in handlers
                   select handler(message);
        }
    }
}