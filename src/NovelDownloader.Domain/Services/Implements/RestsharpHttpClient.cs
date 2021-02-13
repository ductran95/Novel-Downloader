using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Services.Abstractions;
using RestSharp;

namespace NovelDownloader.Domain.Services.Implements
{
    public class RestsharpHttpClient: IHttpClient
    {
        private readonly ILogger<RestsharpHttpClient> _logger;
        private readonly RestClient _restClient;

        public RestsharpHttpClient(ILogger<RestsharpHttpClient> logger)
        {
            _logger = logger;
            _restClient = new RestClient();
        }
        
        public async Task<string> Get(string url, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting data from url {url} ...", url);
            
            var request = new RestRequest(url);
            var data = await _restClient.GetAsync<string>(request, cancellationToken);
                
            _logger.LogInformation("Get data success!");
            return data;
        }

        public async Task<(byte[] Data, string Name)> DownloadFile(string url, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting data from url {url} ...", url);
            
            var request = new RestRequest(url);
            var data = await _restClient.ExecuteGetAsync(request, cancellationToken);

            var headers = data.Headers;
            var fileContent = data.RawBytes;
            var fileName = url.Split("/").Last();
                
            _logger.LogInformation("Get data success!");
            return (fileContent, fileName);
        }
    }
}