using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBusMagic.Support
{
    internal class MessageHandlers
    {
        private readonly ConcurrentDictionary<Type, BlockingCollection<object>> MessageTypes;

        internal MessageHandlers()
        {
            MessageTypes = new ConcurrentDictionary<Type, BlockingCollection<object>>();
        }

        public void AddHandler<TMessage>(Func<TMessage, Task> handler) where TMessage : IMessage
        {
            var handlers = GetOrAddMessageType<TMessage>();

            handlers.Add(handler);
        }

        private BlockingCollection<object> GetOrAddMessageType<TMessage>() where TMessage : IMessage
        {
            return MessageTypes.GetOrAdd(typeof(TMessage), new BlockingCollection<object>());
        }

        public IEnumerable<Func<TMessage, Task>> GetHandlers<TMessage>() where TMessage : IMessage
        {
            var handlers = GetOrAddMessageType<TMessage>();

            return handlers.Cast<Func<TMessage, Task>>();
        }
    }
}