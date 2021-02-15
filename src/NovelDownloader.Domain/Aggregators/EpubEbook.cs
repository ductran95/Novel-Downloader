using System;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Aggregators
{
    public class EpubEbook: Ebook
    {
        public override EbookFormatEnum Format => EbookFormatEnum.Epub;
        public override IEbookWriter GetWriter(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IEbookWriter<EpubEbook>>();
        }
    }
}