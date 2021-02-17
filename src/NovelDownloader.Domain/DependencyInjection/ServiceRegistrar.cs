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
                Init(ServiceCollectionExtension.RegisterServices);
            }
        }
        
        public static void Init(Action<IServiceCollection> registration)
        {
            if (ServiceCollection == null)
            {
                ServiceCollection = new ServiceCollection();
            }
            
            registration.Invoke(ServiceCollection);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public static void Dispose()
        {
            ServiceCollection = null;
            ServiceProvider = null;
        }
    }
}