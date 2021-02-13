using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements.Writer
{
    public class EpubEbookWriter: IEbookWriter<EpubEbook>
    {
        private readonly ILogger<EpubEbookWriter> _logger;

        public EpubEbookWriter(ILogger<EpubEbookWriter> logger)
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