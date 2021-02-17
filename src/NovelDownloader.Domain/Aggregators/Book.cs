using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NovelDownloader.Domain.DependencyInjection;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Aggregators
{
    public abstract class Book: DefaultDependency
    {
        public abstract string Provider { get; }
        
        public string Id { get; set; }
        public string Url { get; set; }
        public BookMetadata Metadata { get; set; }
        public List<Chapter> Chapters { get; set; }
        public Ebook Ebook { get; set; }

        public abstract IBookDownloader GetDownloader();

        public virtual async Task CreateNew(EbookFormatEnum format, string directory = null, CancellationToken cancellationToken = default)
        {
            var downloader = GetDownloader();
            var formatChecker = ServiceProvider.GetService<IEbookFormatChecker>();
            var configManager = ServiceProvider.GetService<IConfigManager>();
            
            await downloader.InitBookMetadata(this);
            this.Ebook = formatChecker.GetEbook(this.Metadata.Name, format, directory);

            await downloader.InitBookChapters(this);

            var writer = this.Ebook.GetWriter();

            await writer.Init(this.Ebook);
            
            // Write metadata
            await writer.WriteMetadata(this.Ebook, this.Metadata);

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
            
            await writer.WriteLastChapterMetadata(this.Ebook, chapter);

            // Build TOC
            await writer.UpdateTOC(this.Ebook);

            // Update Style
            var styles = await configManager.GetBookStyles();
            await writer.UpdateStyle(this.Ebook, styles);
            
            // Save
            await writer.Save(this.Ebook);
        }

        public virtual async Task Update(string path, CancellationToken cancellationToken = default)
        {
            var downloader = GetDownloader();
            var formatChecker = ServiceProvider.GetService<IEbookFormatChecker>();
            var configManager = ServiceProvider.GetService<IConfigManager>();
            
            await downloader.InitBookMetadata(this);
            this.Ebook = formatChecker.GetEbook(path);

            await downloader.InitBookChapters(this);

            var writer = this.Ebook.GetWriter();

            await writer.Init(this.Ebook);

            var lastChapter = await writer.GetLastChapter(this.Ebook);

            // Write each chapter from lastChapter
            var chapter = this.Chapters.FirstOrDefault(x => x.Name.ToLower() == lastChapter.Name?.ToLower());
            if (chapter == null)
            {
                chapter = this.Chapters.FirstOrDefault(x => x.Url.ToLower() == lastChapter.Url?.ToLower());
            }
            if (chapter == null)
            {
                chapter = this.Chapters.FirstOrDefault(x => x.Number == lastChapter.Number);
            }
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
            
            await writer.WriteLastChapterMetadata(this.Ebook, chapter);

            // Build TOC
            await writer.UpdateTOC(this.Ebook);

            // Update Style
            var styles = await configManager.GetBookStyles();
            await writer.UpdateStyle(this.Ebook, styles);
            
            // Save
            await writer.Save(this.Ebook);
        }
    }
}