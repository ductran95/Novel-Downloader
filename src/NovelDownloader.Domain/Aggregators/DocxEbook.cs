using System;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Aggregators
{
    public class DocxEbook: Ebook
    {
        public override EbookFormatEnum Format => EbookFormatEnum.Docx;
        public override IEbookWriter GetWriter() => ServiceProvider.GetService<IEbookWriter<DocxEbook>>();
        
    }
}