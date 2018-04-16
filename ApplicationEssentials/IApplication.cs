using System;

using Ninject;

namespace Maad.ApplicationEssentials
{
    public interface IApplication
    {
        void RegisterServices(IKernel kernel);


        void HandleException(Exception exception);
    }
}