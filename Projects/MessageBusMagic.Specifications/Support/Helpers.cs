using System;

namespace MessageBusMagic.Specifications.Support
{
    internal class Helpers
    {
        internal static TException Catch<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
                return null;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerExceptions.Count == 1 && aggregateException.InnerExceptions[0].GetType() == typeof(TException))
                {
                    return (TException) aggregateException.InnerExceptions[0];
                }
                throw;
            }
            catch (TException exception)
            {
                return exception;
            }
        }
    }
}
