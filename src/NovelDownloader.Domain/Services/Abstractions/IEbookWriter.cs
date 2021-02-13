using System.Threading.Tasks;
using NovelDownloader.Domain.Aggregators;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IEbookWriter<T> where T: Ebook
    {
        Task Init(Ebook ebook);
        string GetLastChapter(Ebook ebook);
    }
}