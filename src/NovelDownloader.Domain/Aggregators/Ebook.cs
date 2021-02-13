using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Aggregators
{
    public abstract class Ebook
    {
        public string FilePath { get; set; }
        public abstract EbookFormatEnum Format { get; }
    }
}