using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Maad.ApplicationEssentials
{
    public abstract class AbstractApplication
    {
        public Microsoft.Extensions.Logging.ILogger Logger { get; private set; }

        public AbstractApplication(ILogger<AbstractApplication> logger)
        {
            Logger = logger;
        }

        protected static void RunApplication()
        {
            try
            {
                StackFrame frame = new StackFrame(1);

            var type = frame.GetMethod().DeclaringType;

            var applicationInitializer = Activator.CreateInstance(type, new NullInstanceLogger<AbstractApplication>()) as AbstractApplication;

            IServiceCollection services = new ServiceCollection();
            services.AddLogging(applicationInitializer.ConfigureLogging);
            var serviceProvider = applicationInitializer.RegisterServices(services);

                var application = serviceProvider.GetRequiredService<AbstractApplication>();

                application.Start(serviceProvider);
            }
            catch (Exception ex)
            {
                File.WriteAllText("crash.log", $"[{DateTime.Now}] - {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                Environment.Exit(-1);
            }
        }


        private void Start(IServiceProvider serviceProvider)
        {
            try
            {
                Logger.LogInformation($"Starting application {Assembly.GetEntryAssembly().GetName().Name}.");

                Run(serviceProvider);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, ex.Message);
            }
        }


        protected abstract void ConfigureLogging(ILoggingBuilder loggingBuilder);


        internal protected abstract IServiceProvider RegisterServices(IServiceCollection services);


        public abstract void Run(IServiceProvider serviceProvider);
    }
}