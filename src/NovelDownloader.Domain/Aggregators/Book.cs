using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Aggregators
{
    public abstract class Book
    {
        public abstract string Provider { get; }
        
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public List<string> Categories { get; set; }
        public byte[] Cover { get; set; }
        public string CoverExt { get; set; }
        
        public int TotalChapter { get; set; }
        public BookTypeEnum BookType { get; set; }
        
        public List<Chapter> Chapters { get; set; }
        public Ebook Ebook { get; set; }

        public abstract Task Download(IServiceProvider serviceProvider,  CancellationToken cancellationToken=default);
    }
}