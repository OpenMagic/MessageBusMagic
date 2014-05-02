using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBusMagic.Support
{
    internal class MessageHandlers
    {
        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<int, Func<IMessage, Task>>> Handlers;

        internal MessageHandlers()
        {
            Handlers = new ConcurrentDictionary<Type, ConcurrentDictionary<int, Func<IMessage, Task>>>();
        }

        internal void SubscribeTo<TMessage>(Func<IMessage, Task> handler) where TMessage : IMessage
        {
            var handlers = GetOrAddHandlersFor(typeof(TMessage));

            if (handlers.TryAdd(handler.GetHashCode(), handler))
            {
                return;
            }

            var exception = new Exception("Cannot subscribe the a handler more than once.");

            exception.Data.Add("TMessage", typeof(TMessage));
            exception.Data.Add("handler", handler);

            throw exception;
        }

        private ConcurrentDictionary<int, Func<IMessage, Task>> GetOrAddHandlersFor(Type messageType)
        {
            return Handlers.GetOrAdd(messageType, key => new ConcurrentDictionary<int, Func<IMessage, Task>>());
        }


        internal IEnumerable<Func<IMessage, Task>> FindHandlersFor<TMessage>() where TMessage : IMessage
        {
            ConcurrentDictionary<int, Func<IMessage, Task>> subscribers;

            return Handlers.TryGetValue(typeof(TMessage), out subscribers) ? subscribers.Values : Enumerable.Empty<Func<IMessage, Task>>();
        }
    }
}
