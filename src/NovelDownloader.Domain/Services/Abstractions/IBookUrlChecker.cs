using NovelDownloader.Domain.Aggregators;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IBookUrlChecker
    {
        Book CreateBook(string url);
    }
}