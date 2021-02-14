using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;

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

        public abstract IBookDownloader GetDownloader(IServiceProvider serviceProvider);

        public virtual async Task CreateNew(IServiceProvider serviceProvider, EbookFormatEnum format, string directory = null, CancellationToken cancellationToken = default)
        {

            var downloader = GetDownloader(serviceProvider);
            var formatChecker = serviceProvider.GetService<IEbookFormatChecker>();
            
            await downloader.InitBookMetadata(this);
            this.Ebook = formatChecker.GetEbook(this.Name, format, directory);

            await downloader.InitBookChapters(this);

            var writer = this.Ebook.GetWriter(serviceProvider);

            await writer.Init(this.Ebook);
            
            // Write cover
            
            // Write each chapter
            
            // Build TOC
            
            // Update Style
        }

        public virtual async Task Update(IServiceProvider serviceProvider, string path, CancellationToken cancellationToken = default)
        {
            var downloader = GetDownloader(serviceProvider);
            var formatChecker = serviceProvider.GetService<IEbookFormatChecker>();
            
            await downloader.InitBookMetadata(this);
            this.Ebook = formatChecker.GetEbook(path);

            await downloader.InitBookChapters(this);

            var writer = this.Ebook.GetWriter(serviceProvider);

            await writer.Init(this.Ebook);

            var lastChapter = await writer.GetLastChapter(this.Ebook);

            // Write each chapter from lastChapter

            // Build TOC
            
            // Update Style
        }
    }
}