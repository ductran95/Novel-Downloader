using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Extensions;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements.Downloader
{
    public class TTVBookDownloader: IBookDownloader<TTVBook>
    {
        private const string GetTOCUrl = "https://truyen.tangthuvien.vn/doc-truyen/page/{0}?page={1}&limit=100&web=1";
        
        private readonly ILogger<TTVBookDownloader> _logger;
        private readonly IHttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;

        public TTVBookDownloader(ILogger<TTVBookDownloader> logger, IHttpClient httpClient, IHtmlParser htmlParser)
        {
            _logger = logger;
            _httpClient = httpClient;
            _htmlParser = htmlParser;
        }
        
        public async Task InitBookMetadata(Book book)
        {
            _logger.LogInformation("Getting book metadata ...");
            book.Metadata = new BookMetadata();
            book.Metadata.Provider = book.Provider;
            
            var bookHomePageRes = await _httpClient.Get(book.Url);
            var bookHomePage = _htmlParser.Parse(bookHomePageRes).DocumentNode;
            _logger.LogInformation("Request book homepage success");
            
            var idElement = bookHomePage.SelectSingleNode("//meta[@name='book_detail']");
            book.Id = idElement.GetAttributeValue("content", string.Empty);
            _logger.LogInformation("Get book id success");

            var titleElement = bookHomePage.QuerySelector("div.book-info > h1");
            book.Metadata.Name = titleElement.InnerText;
            _logger.LogInformation("Get book title success");
            
            var tagParentElement = bookHomePage.QuerySelector("div.book-info > p.tag");
            var tags = tagParentElement.ChildNodes.Select(x => x.InnerText);
            book.Metadata.Author = tags.FirstOrDefault();
            book.Metadata.Categories = tags.Skip(1).ToList();
            _logger.LogInformation("Get book author, categories success");
            
            var descriptionElements = bookHomePage.QuerySelectorAll("#gioithieu > p");
            book.Metadata.Description = string.Join(Environment.NewLine, descriptionElements.Select(x=>x.InnerText));
            _logger.LogInformation("Get book description success");

            var coverElement = bookHomePage.QuerySelector("#bookImg > img");
            var coverFile = await _httpClient.DownloadFile(coverElement.GetAttributeValue("src", string.Empty));
            book.Metadata.Cover = coverFile.Data;
            book.Metadata.CoverName = coverFile.Name;
            _logger.LogInformation("Get book cover success");
            
            var totalChapterElement = bookHomePage.QuerySelector("#j-bookCatalogPage");
            var totalChapterText = totalChapterElement.InnerText;
            var totalChapterNumber = totalChapterText.GetNumbers().FirstOrDefault();
            book.Metadata.TotalChapter = totalChapterNumber;
            _logger.LogInformation("Get book total chapter success");
            
            _logger.LogInformation("Get book metadata success");
        }
        
        public async Task InitBookChapters(Book book)
        {
            _logger.LogInformation("Getting book chapters ...");

            int page = 0;
            int number = 1;
            bool hasChapter = false;

            book.Chapters = new List<Chapter>();
            
            do
            {
                hasChapter = false;
                
                var bookChaptersRes = await _httpClient.Get(string.Format(GetTOCUrl, book.Id, page.ToString()));
                var bookChaptersPage = _htmlParser.Parse(bookChaptersRes).DocumentNode;
                _logger.LogInformation($"Request book chapters page {page} success");
                
                var chapterElements = bookChaptersPage.QuerySelectorAll("body > ul > li > a");

                hasChapter = chapterElements.Any();

                foreach (var chapterElement in chapterElements)
                {
                    var href = chapterElement.GetAttributeValue("href", string.Empty);
                    var title = chapterElement.GetAttributeValue("title", string.Empty);

                    title = title.Replace("&nbsp;", " ");
                    
                    book.Chapters.Add(new Chapter()
                    {
                        Number = number,
                        Name = title,
                        Url = href
                    });

                    number++;
                }
                
                page++;
            } while (hasChapter);
            
            _logger.LogInformation("Get book chapters success");
        }

        public async Task GetChapter(Chapter chapter)
        {
            _logger.LogInformation($"Getting chapter {chapter.Name} ...");
            
            var chapterPageRes = await _httpClient.Get(chapter.Url);
            var chapterPage = _htmlParser.Parse(chapterPageRes).DocumentNode;
            _logger.LogInformation("Request chapter success");
            
            var chapterContentElement = chapterPage.QuerySelector("div.chapter-c > div.chapter-c-content > div.box-chap");
            var chapterText = chapterContentElement.InnerText;
            var paragraphs = chapterText.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            chapter.Paragraphs = paragraphs.ToList();
            _logger.LogInformation("Get chapter paragraphs success");
            
            _logger.LogInformation("Get chapter success");
        }
    }
}