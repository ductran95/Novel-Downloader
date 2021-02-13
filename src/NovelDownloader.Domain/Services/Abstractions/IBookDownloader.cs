using System.Threading.Tasks;
using NovelDownloader.Domain.Aggregators;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IBookDownloader<T> where T: Book
    {
        Task InitBookMetadata(Book book);
        Task InitBookChapters(Book book);
        Task GetChapter(Chapter chapter);
    }
}