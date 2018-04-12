using Ninject;

namespace Maad.ApplicationEssentials
{
    public interface ISynchronousApplication : IApplication
    {
        void Run(IKernel kernel);
    }
}
