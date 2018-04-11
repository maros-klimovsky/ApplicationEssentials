using System;
using System.Reflection;
using System.Threading.Tasks;

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


        protected static IKernel Kernel
        {
            private get;
            set;
        }


        public async static Task StartAsync(string[] args = null)
        {
            var application = new TApplication();

            try
            {
                Kernel = Kernel ?? new StandardKernel();

                using (Kernel)
                {
                    Kernel.Load<LoggingModule>();

                    application.RegisterServices(Kernel);

                    InitializeLogging();

                    Logger.LogDebug($"Starting application {Assembly.GetEntryAssembly().GetName().Name}");

                    await application.RunAsync(Kernel);
                }

            }
            catch (Exception ex)
            {
                application.HandleException(ex);
            }
        }

        public static void Start(string[] args = null)
        {
            StartAsync(args).GetAwaiter().GetResult();
        }

        public abstract Task RunAsync(IKernel kernel);


        public abstract void RegisterServices(IKernel kernel);


        public virtual void HandleException(Exception exception)
        {
            Logger.LogCritical("Unhandled exception. Application shutting down.", exception);
        }


        private static void InitializeLogging()
        {
            ILoggerFactory loggerFactory = Kernel.Get<ILoggerFactory>();

            Logger = loggerFactory.CreateLogger<TApplication>();
        }
    }
}
