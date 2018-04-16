using System.Threading.Tasks;

using Ninject;

namespace Maad.ApplicationEssentials
{
    public interface IAsynchronousApplication : IApplication
    {
        Task RunAsync(IKernel kernel);
    }
}