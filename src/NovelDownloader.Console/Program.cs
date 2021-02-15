using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NovelDownloader.Domain.Services.Abstractions;
using NovelDownloader.Domain.Services.Implements;

namespace NovelDownloader.Console
{
    class Program
    {
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    {
                        services
                            .AddHostedService<Worker>()
                            .AddSingleton<IBookUrlChecker, DefaultBookUrlChecker>();
                    });
    }
}