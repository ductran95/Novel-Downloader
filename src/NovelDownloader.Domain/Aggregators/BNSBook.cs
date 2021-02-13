using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovelDownloader.Domain.Aggregators
{
    public class BNSBook: Book
    {
        public override string Provider => "Bach Ngoc Sach";
        
        public override async Task Download(IServiceProvider serviceProvider,CancellationToken cancellationToken=default)
        {
            throw new System.NotImplementedException();
        }
    }
}