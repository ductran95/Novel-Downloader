using System;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements
{
    public class DefaultBookUrlChecker: IBookUrlChecker
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DefaultBookUrlChecker> _logger;

        public DefaultBookUrlChecker(IServiceProvider serviceProvider, ILogger<DefaultBookUrlChecker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public Book CreateBook(string url)
        {
            _logger.LogInformation("Checking book url {url} ...", url);
            
            Book book;

            bool checkUrl = Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!checkUrl)
            {
                throw new ArgumentException($"{url} is not a valid URL");
            }

            var site = uriResult.Host;

            switch (site)
            {
                case "truyen.tangthuvien.vn":
                    var ttvUri = new UriBuilder(url)
                    {
                        Scheme = Uri.UriSchemeHttps
                    };

                    book = new TTVBook()
                    {
                        Url = ttvUri.ToString()
                    };
                    
                    break;
                    
                case "bachngocsach.com":
                    var bnsUri = new UriBuilder(url)
                    {
                        Scheme = Uri.UriSchemeHttps
                    };

                    book = new BNSBook()
                    {
                        Url = bnsUri.ToString()
                    };
                    break;
                
                default:
                    throw new NotSupportedException("Only support TTV and BNS");
            }

            book.ServiceProvider = _serviceProvider;
            _logger.LogInformation("Check book url success!");

            return book;
        }
    }
}