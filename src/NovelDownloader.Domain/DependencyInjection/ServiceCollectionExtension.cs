using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;
using NovelDownloader.Domain.Services.Implements;
using NovelDownloader.Domain.Services.Implements.Downloader;
using NovelDownloader.Domain.Services.Implements.Writer;

namespace NovelDownloader.Domain.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterNovelDownloader(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterServices();
            ServiceRegistrar.Dispose();
            
            return serviceCollection;
        }
        
        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IBookUrlChecker, DefaultBookUrlChecker>()
                .AddSingleton<IHttpClient, RestsharpHttpClient>()
                .AddSingleton<IHtmlParser, HAPHtmlParser>()
                .AddSingleton<IBookDownloader<TTVBook>, TTVBookDownloader>()
                .AddSingleton<IBookDownloader<BNSBook>, BNSBookDownloader>()
                .AddSingleton<IEbookFormatChecker, DefaultEbookFormatChecker>()
                .AddSingleton<IEbookWriter<DocxEbook>, DocxEbookWriter>()
                .AddSingleton<IEbookWriter<EpubEbook>, EpubEbookWriter>()
                .AddSingleton<IConfigManager, DefaultStyleConfigManager>();
            
            return serviceCollection;
        }
    }
}