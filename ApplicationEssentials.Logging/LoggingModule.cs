using Microsoft.Extensions.Logging;
using Ninject;
using Ninject.Modules;

namespace Maad.ApplicationEssentials.Logging
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ILoggerFactory>().ToMethod((context) =>
            {
                var factory = Kernel.Get<LoggerFactory>();
                factory.AddLog4Net();

                return factory;
            });
        }
    }
}
