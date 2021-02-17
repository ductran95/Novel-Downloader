using System;

namespace NovelDownloader.Domain.DependencyInjection
{
    public abstract class DefaultDependency: IDependency
    {
        public IServiceProvider ServiceProvider { get; set; } = ServiceRegistrar.ServiceProvider;
    }
}