using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace NovelDownloader.Console
{
    public class Worker: IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}