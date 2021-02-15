using System;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;
using Xceed.Words.NET;

namespace NovelDownloader.Domain.Aggregators
{
    public class DocxEbook: Ebook
    {
        public override EbookFormatEnum Format => EbookFormatEnum.Docx;
        public DocX Document { get; set; }
        public override IEbookWriter GetWriter(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IEbookWriter<DocxEbook>>();
        }
    }
}