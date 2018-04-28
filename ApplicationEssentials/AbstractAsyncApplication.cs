using System;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Ninject;

namespace Maad.ApplicationEssentials
{
    public abstract class AbstractAsyncApplication<TApplication> : ApplicationFoundation, IAsynchronousApplication where TApplication : IAsynchronousApplication, new()
    {
        public static async Task StartAsync()
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

                    await application.RunAsync(Kernel);
                }

            }
            catch (Exception ex)
            {
                application.HandleException(ex);
            }
        }


        public abstract Task RunAsync(IKernel kernel);


    }
}