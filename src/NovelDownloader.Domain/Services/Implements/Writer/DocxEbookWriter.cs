using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
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
            throw new System.NotImplementedException();
        }

        public async Task<string> GetLastChapter(Ebook ebook)
        {
            throw new System.NotImplementedException();
        }

        public async Task WriteCover(Ebook ebook)
        {
            throw new System.NotImplementedException();
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