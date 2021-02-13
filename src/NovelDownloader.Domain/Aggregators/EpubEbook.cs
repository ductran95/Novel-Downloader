using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Aggregators
{
    public class EpubEbook: Ebook
    {
        public override EbookFormatEnum Format => EbookFormatEnum.Epub;
    }
}