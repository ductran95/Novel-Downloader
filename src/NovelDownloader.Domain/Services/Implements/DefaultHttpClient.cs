using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements
{
    public class DefaultHttpClient: IHttpClient
    {
        private readonly ILogger<DefaultHttpClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly CookieContainer _cookie;

        public DefaultHttpClient(ILogger<DefaultHttpClient> logger)
        {
            _logger = logger;
            
            _cookie = new CookieContainer();
            
            _httpClientHandler = new HttpClientHandler
            {
                CookieContainer = _cookie,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                AllowAutoRedirect = true,
                UseDefaultCredentials = false
            };

            _httpClient = new HttpClient(_httpClientHandler);
        }
        
        public async Task<string> Get(string url, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting data from url {url} ...", url);
            
            var data = await _httpClient.GetStringAsync(url, cancellationToken);
            
            _logger.LogInformation("Get data success!");
            return data;
        }

        public async Task<(byte[] Data, string Name)> DownloadFile(string url, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Downloading file from url {url} ...", url);

            var data = await _httpClient.GetAsync(url, cancellationToken);

            var headers = data.Headers;
            var content = data.Content;

            var fileContent = await content.ReadAsByteArrayAsync(cancellationToken);
            var fileName = url.Split("/").Last();
            
            _logger.LogInformation("Download file success!");
            return (fileContent, fileName);
        }
    }
}