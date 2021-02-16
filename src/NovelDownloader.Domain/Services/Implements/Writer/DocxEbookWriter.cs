using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Constants;
using NovelDownloader.Domain.Services.Abstractions;

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
                
            }
            else
            {
            }
        }

        public async Task<Chapter> GetLastChapter(Ebook ebook)
        {
            var docxBook = ebook as DocxEbook;

            if (ebook.IsExisted)
            {
                var result = new Chapter();

                return result;
            }

            return null;
        }

        public async Task WriteMetadata(Ebook ebook, BookMetadata metadata)
        {
            var docxBook = ebook as DocxEbook;

            if (!ebook.IsExisted)
            {
                
            }
        }

        public async Task WriteChapter(Ebook ebook, Chapter chapter)
        {
            
        }

        public async Task WriteLastChapterMetadata(Ebook ebook, Chapter lastChapter)
        {
            
        }

        public async Task UpdateTOC(Ebook ebook)
        {
            
        }

        public async Task UpdateStyle(Ebook ebook, IEnumerable<BookStyle> styles)
        {
            
        }

        public async Task Save(Ebook ebook)
        {
            
        }
    }
}