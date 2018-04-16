using System;

using Microsoft.Extensions.Logging;
using Ninject;

namespace Maad.ApplicationEssentials
{
    public abstract class ApplicationFoundation : IApplication
    {
        protected static ILogger Logger
        {
            get;
            private set;
        }


        internal static IKernel Kernel
        {
            get;
            set;
        }


        public abstract void RegisterServices(IKernel kernel);


        public virtual void HandleException(Exception exception)
        {
            Logger.LogCritical("Unhandled exception. Application shutting down.", exception);
        }


        internal static void InitializeLogging<TApplication>()
        {
            ILoggerFactory loggerFactory = Kernel.Get<ILoggerFactory>();

            Logger = loggerFactory.CreateLogger<TApplication>();
        }
    }
}