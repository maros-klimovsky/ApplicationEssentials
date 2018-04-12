using System;
using System.Threading.Tasks;

using Ninject;

namespace Maad.ApplicationEssentials
{
    public interface IApplication
    {
        void RegisterServices(IKernel kernel);


        void HandleException(Exception exception);
    }
}
