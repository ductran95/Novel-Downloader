using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IEbookFormatChecker
    {
        Ebook GetEbook(string path);
        Ebook GetEbook(string bookName, EbookFormatEnum format, string directory = null);
    }
}