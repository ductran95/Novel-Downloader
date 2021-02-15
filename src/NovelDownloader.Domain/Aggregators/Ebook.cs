using System;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Aggregators
{
    public abstract class Ebook
    {
        public string FilePath { get; set; }
        public bool IsExisted { get; set; }
        public abstract EbookFormatEnum Format { get; }
        
        public abstract IEbookWriter GetWriter(IServiceProvider serviceProvider);
    }
}