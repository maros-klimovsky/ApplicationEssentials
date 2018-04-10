using System;
using System.Reflection;

using Microsoft.Extensions.Logging;
using Ninject;

using Maad.ApplicationEssentials.Logging;

namespace Maad.ApplicationEssentials
{
    public abstract class AbstractApplication<TApplication> : IApplication where TApplication : IApplication, new()
    {
        protected static ILogger Logger
        {
            get;
            private set;
        }


        public static void Start(IKernel kernel = null)
        {
            var application = new TApplication();

            try
            {
                kernel = kernel ?? new StandardKernel();

                using (kernel)
                {
                    kernel.Load<LoggingModule>();

                    application.RegisterServices(kernel);

                    InitializeLogging(kernel);

                    Logger.LogDebug($"Starting application {Assembly.GetEntryAssembly().GetName().Name}");

                    application.Run(kernel);
                }

            }
            catch (Exception ex)
            {
                application.HandleException(ex);
            }
        }


        public abstract void Run(IKernel kernel);


        public abstract void RegisterServices(IKernel kernel);


        public virtual void HandleException(Exception exception)
        {
            Logger.LogCritical("Unhandled exception. Application shutting down.", exception);
        }


        private static void InitializeLogging(IKernel kernel)
        {
            ILoggerFactory loggerFactory = kernel.Get<ILoggerFactory>();

            Logger = loggerFactory.CreateLogger<TApplication>();
        }
    }
}
