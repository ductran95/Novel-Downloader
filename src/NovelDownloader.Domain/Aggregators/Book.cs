using System;
using System.Collections.Generic;
using System.Linq;
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
            var configManager = serviceProvider.GetService<IConfigManager>();
            
            await downloader.InitBookMetadata(this);
            this.Ebook = formatChecker.GetEbook(this.Name, format, directory);

            await downloader.InitBookChapters(this);

            var writer = this.Ebook.GetWriter(serviceProvider);

            await writer.Init(this.Ebook);
            
            // Write cover
            await writer.WriteCover(this.Ebook, this.Name, this.Author, this.Categories, this.Cover, this.CoverExt);

            // Write each chapter
            var chapter = this.Chapters[0];
            var getChapterTask = downloader.GetChapter(chapter);
            await getChapterTask;
            
            for (int i = 1; i < this.Chapters.Count; i++)
            {
                getChapterTask = downloader.GetChapter(this.Chapters[i]);

                await writer.WriteChapter(this.Ebook, chapter);
                await getChapterTask;
                chapter = this.Chapters[i];
            }
            
            // Because the last chapter only be downloaded in for loop
            // So we have to write that chapter after.
            await writer.WriteChapter(this.Ebook, chapter);

            // Build TOC
            await writer.UpdateTOC(this.Ebook);

            // Update Style
            var styles = await configManager.GetBookStyles();
            await writer.UpdateStyle(this.Ebook, styles);
        }

        public virtual async Task Update(IServiceProvider serviceProvider, string path, CancellationToken cancellationToken = default)
        {
            var downloader = GetDownloader(serviceProvider);
            var formatChecker = serviceProvider.GetService<IEbookFormatChecker>();
            var configManager = serviceProvider.GetService<IConfigManager>();
            
            await downloader.InitBookMetadata(this);
            this.Ebook = formatChecker.GetEbook(path);

            await downloader.InitBookChapters(this);

            var writer = this.Ebook.GetWriter(serviceProvider);

            await writer.Init(this.Ebook);

            var lastChapter = await writer.GetLastChapter(this.Ebook);

            // Write each chapter from lastChapter
            var chapter = this.Chapters.FirstOrDefault(x => x.Name.ToLower() == lastChapter.ToLower());
            if (chapter == null)
            {
                chapter = this.Chapters[0];
            }
            
            var getChapterTask = downloader.GetChapter(chapter);
            await getChapterTask;
            
            for (int i = 1; i < this.Chapters.Count; i++)
            {
                getChapterTask = downloader.GetChapter(this.Chapters[i]);

                await writer.WriteChapter(this.Ebook, chapter);
                await getChapterTask;
                chapter = this.Chapters[i];
            }
            
            // Because the last chapter only be downloaded in for loop
            // So we have to write that chapter after.
            await writer.WriteChapter(this.Ebook, chapter);

            // Build TOC
            await writer.UpdateTOC(this.Ebook);

            // Update Style
            var styles = await configManager.GetBookStyles();
            await writer.UpdateStyle(this.Ebook, styles);
        }
    }
}