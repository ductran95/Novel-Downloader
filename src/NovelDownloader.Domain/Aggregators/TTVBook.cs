using System;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Aggregators
{
    public class TTVBook: Book
    {
        public override string Provider => "Tang Thu Vien";
        public override IBookDownloader GetDownloader(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IBookDownloader<TTVBook>>();
        }
    }
}