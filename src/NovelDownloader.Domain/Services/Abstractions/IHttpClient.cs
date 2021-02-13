using System.Threading;
using System.Threading.Tasks;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IHttpClient
    {
        Task<string> Get(string url, CancellationToken cancellationToken = default);
        Task<(byte[] Data, string Name)> DownloadFile(string url, CancellationToken cancellationToken = default);
    }
}