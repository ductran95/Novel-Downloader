using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements.Downloader
{
    public class BNSBookDownloader: IBookDownloader<BNSBook>
    {
        private readonly ILogger<BNSBookDownloader> _logger;
        private readonly IHttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;

        public BNSBookDownloader(ILogger<BNSBookDownloader> logger, IHttpClient httpClient, IHtmlParser htmlParser)
        {
            _logger = logger;
            _httpClient = httpClient;
            _htmlParser = htmlParser;
        }
        
        public async Task InitBookMetadata(Book book)
        {
            throw new System.NotImplementedException();
        }

        public async Task InitBookChapters(Book book)
        {
            throw new System.NotImplementedException();
        }

        public async Task GetChapter(Chapter chapter)
        {
            throw new System.NotImplementedException();
        }
    }
}