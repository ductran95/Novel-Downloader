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

        public string GetLastChapter(Ebook ebook)
        {
            throw new System.NotImplementedException();
        }
    }
}