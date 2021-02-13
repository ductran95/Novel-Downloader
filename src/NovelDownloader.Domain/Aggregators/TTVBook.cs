using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovelDownloader.Domain.Aggregators
{
    public class TTVBook: Book
    {
        public override string Provider => "Tang Thu Vien";
        
        public override async Task Download(IServiceProvider serviceProvider,CancellationToken cancellationToken=default)
        {
            throw new System.NotImplementedException();
        }
    }
}