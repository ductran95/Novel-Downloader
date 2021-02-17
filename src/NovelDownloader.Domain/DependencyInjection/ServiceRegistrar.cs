using System;
using Microsoft.Extensions.DependencyInjection;

namespace NovelDownloader.Domain.DependencyInjection
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection ServiceCollection { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        static ServiceRegistrar()
        {
            Init();
        }
        
        public static void Init()
        {
            if (ServiceProvider == null)
            {
                ServiceCollection = new ServiceCollection();
                ServiceCollection.RegisterServices();
                ServiceProvider = ServiceCollection.BuildServiceProvider();
            }
        }

        public static void Dispose()
        {
            ServiceCollection = null;
            ServiceProvider = null;
        }
    }
}