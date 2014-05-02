using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageBusMagic
{
    /// <summary>
    ///     A MessageBus!
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        ///     todo: document
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="action">The action.</param>
        Task SubscribeTo<TMessage>(Func<IMessage, Task> action) where TMessage : IMessage;

        /// <summary>
        ///     todo: document
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        IEnumerable<Task> Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }
}