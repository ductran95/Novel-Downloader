using System.Collections.Generic;
using System.Threading.Tasks;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IEbookWriter
    {
        Task Init(Ebook ebook);
        Task<string> GetLastChapter(Ebook ebook);
        Task WriteMetadata(Ebook ebook, BookMetadata metadata);
        Task WriteChapter(Ebook ebook, Chapter chapter);
        Task WriteLastChapterMetadata(Ebook ebook, Chapter lastChapter);
        Task UpdateTOC(Ebook ebook);
        Task UpdateStyle(Ebook ebook, IEnumerable<BookStyle> styles);
    }
    
    public interface IEbookWriter<T>: IEbookWriter where T: Ebook
    {
        
    }
}