using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;
using Xceed.Words.NET;

namespace NovelDownloader.Domain.Services.Implements.Writer
{
    public class DocxEbookWriter: IEbookWriter<DocxEbook>
    {
        private readonly ILogger<DocxEbookWriter> _logger;

        public DocxEbookWriter(ILogger<DocxEbookWriter> logger)
        {
            _logger = logger;
        }
        
        public async Task Init(Ebook ebook)
        {
            var docxBook = ebook as DocxEbook;
            
            if (docxBook.IsExisted)
            {
                docxBook.Document = DocX.Load(ebook.FilePath);
            }
            else
            {
                docxBook.Document = DocX.Create(ebook.FilePath);
            }
        }

        public async Task<string> GetLastChapter(Ebook ebook)
        {
            var docxBook = ebook as DocxEbook;

            if (ebook.IsExisted)
            {
                return docxBook.Document.CoreProperties["LastChapter"];
            }

            return string.Empty;
        }

        public async Task WriteCover(Ebook ebook, string title, string author, IEnumerable<string> categories, byte[] cover, string coverExt)
        {
            var docxBook = ebook as DocxEbook;

            if (!ebook.IsExisted)
            {
                
            }
        }

        public async Task WriteChapter(Ebook ebook, Chapter chapter)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateTOC(Ebook ebook)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateStyle(Ebook ebook, IEnumerable<BookStyle> styles)
        {
            throw new System.NotImplementedException();
        }
    }
}