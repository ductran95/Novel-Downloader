using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Aggregators
{
    public class DocxEbook: Ebook
    {
        public override EbookFormatEnum Format => EbookFormatEnum.Docx;
    }
}