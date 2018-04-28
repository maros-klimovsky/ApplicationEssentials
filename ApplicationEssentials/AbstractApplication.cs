using System;
using System.Reflection;

using Microsoft.Extensions.Logging;
using Ninject;

namespace Maad.ApplicationEssentials
{
    public abstract class AbstractApplication<TApplication> : ApplicationFoundation, ISynchronousApplication where TApplication : ISynchronousApplication, new()
    {
        public static void Start()
        {
            var application = new TApplication();

            try
            {
                Kernel = Kernel ?? new StandardKernel();

                using(Kernel)
                {
                    Kernel.Load<LoggingModule>();

                    application.RegisterServices(Kernel);

                    InitializeLogging<TApplication>();

                    Logger.LogDebug($"Starting application {Assembly.GetEntryAssembly().GetName().Name}.");

                    application.Run(Kernel);
                }

            }
            catch (Exception ex)
            {
                application.HandleException(ex);
            }
        }


        public abstract void Run(IKernel kernel);
    }
}