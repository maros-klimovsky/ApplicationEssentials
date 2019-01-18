using System;

using Microsoft.Extensions.Logging;

namespace Maad.ApplicationEssentials
{
    internal class NullInstanceLogger<TType> : ILogger<TType>
        where TType : class
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
