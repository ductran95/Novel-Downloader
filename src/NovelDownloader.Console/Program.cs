using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;
using NovelDownloader.Domain.Services.Implements;
using NovelDownloader.Domain.Services.Implements.Downloader;
using NovelDownloader.Domain.Services.Implements.Writer;
using Serilog;

namespace NovelDownloader.Console
{
    class Program
    {
        static Task Main(string[] args)
        {
            var config = GetConfiguration();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            
            using IHost host = CreateHostBuilder(args).Build();
            return host.RunAsync();
        }
        
        static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services
                        .AddHostedService<TestWorker>()
                        .AddSingleton<IBookUrlChecker, DefaultBookUrlChecker>()
                        .AddSingleton<IHttpClient, RestsharpHttpClient>()
                        .AddSingleton<IHtmlParser, HAPHtmlParser>()
                        .AddSingleton<IBookDownloader<TTVBook>, TTVBookDownloader>()
                        .AddSingleton<IBookDownloader<BNSBook>, BNSBookDownloader>()
                        .AddSingleton<IEbookFormatChecker, DefaultEbookFormatChecker>()
                        .AddSingleton<IEbookWriter<DocxEbook>, DocxEbookWriter>()
                        .AddSingleton<IEbookWriter<EpubEbook>, EpubEbookWriter>()
                        .AddSingleton<IConfigManager, DefaultStyleConfigManager>();
                })
                .UseSerilog();
    }
}