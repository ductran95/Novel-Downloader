using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements
{
    public class HAPHtmlParser: IHtmlParser
    {
        private readonly ILogger<HAPHtmlParser> _logger;

        public HAPHtmlParser(ILogger<HAPHtmlParser> logger)
        {
            _logger = logger;
        }
        
        public HtmlDocument Parse(string data)
        {
            _logger.LogInformation("Parsing HTML string ...");
            _logger.LogDebug("HTML string: {data}", data);
            
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(data);

            _logger.LogInformation("Parse HTML string success!");
            return htmlDoc;
        }
    }
}