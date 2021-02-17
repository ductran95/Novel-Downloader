using System;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Aggregators
{
    public class BNSBook: Book
    {
        public override string Provider => "Bach Ngoc Sach";
        public override IBookDownloader GetDownloader() => ServiceProvider.GetService<IBookDownloader<BNSBook>>();
    }
}