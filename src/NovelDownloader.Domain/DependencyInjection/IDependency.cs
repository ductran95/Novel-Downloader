using System;

namespace NovelDownloader.Domain.DependencyInjection
{
    public interface IDependency
    {
        IServiceProvider ServiceProvider { get; set; }
    }
}