using System;
using System.Threading.Tasks;

using Ninject;

namespace Maad.ApplicationEssentials
{
    public interface IApplication
    {
        Task RunAsync(IKernel kernel);


        void RegisterServices(IKernel kernel);


        void HandleException(Exception exception);
    }
}
