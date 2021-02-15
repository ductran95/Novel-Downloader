using System.Threading.Tasks;
using NovelDownloader.Domain.Aggregators;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IBookDownloader
    {
        Task InitBookMetadata(Book book);
        Task InitBookChapters(Book book);
        Task GetChapter(Chapter chapter);
    }
    
    public interface IBookDownloader<T>: IBookDownloader where T: Book
    {
        
    }
}