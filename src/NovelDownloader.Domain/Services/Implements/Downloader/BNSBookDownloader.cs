using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements.Downloader
{
    public class BNSBookDownloader: IBookDownloader<BNSBook>
    {
        private readonly ILogger<BNSBookDownloader> _logger;
        private readonly IHttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;

        public BNSBookDownloader(ILogger<BNSBookDownloader> logger, IHttpClient httpClient, IHtmlParser htmlParser)
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
            
            // var idElement = bookHomePage.SelectSingleNode("//meta[@name='book_detail']");
            // book.Id = idElement.GetAttributeValue("content", string.Empty);
            // _logger.LogInformation("Get book id success");

            var titleElement = bookHomePage.QuerySelector("#truyen-title");
            book.Metadata.Name = titleElement.InnerText;
            _logger.LogInformation("Get book title success");
            
            var authorElement = bookHomePage.QuerySelector("#tacgia > a");
            book.Metadata.Author = authorElement.InnerText;
            _logger.LogInformation("Get book author success");
            
            var descriptionElement = bookHomePage.QuerySelector("div.book-info-detail > div.book-intro > p");
            book.Metadata.Description = descriptionElement.InnerText;
            _logger.LogInformation("Get book description success");
            
            var categoryElements = bookHomePage.QuerySelectorAll("#theloai > a");
            book.Metadata.Categories = categoryElements.Select(x => x.InnerText).ToList();
            _logger.LogInformation("Get book categories success");

            var coverElement = bookHomePage.QuerySelector("#anhbia > img");
            var coverFile = await _httpClient.DownloadFile(coverElement.GetAttributeValue("src", string.Empty));
            book.Metadata.Cover = coverFile.Data;
            book.Metadata.CoverName = coverFile.Name;
            _logger.LogInformation("Get book cover success");
            
            // var totalChapterElement = bookHomePage.QuerySelector("#j-bookCatalogPage");
            // var totalChapterText = totalChapterElement.InnerText;
            // var totalChapterNumber = totalChapterText.GetNumbers().FirstOrDefault();
            // book.TotalChapter = totalChapterNumber;
            // _logger.LogInformation("Get book total chapter success");
            
            _logger.LogInformation("Get book metadata success");
        }

        public async Task InitBookChapters(Book book)
        {
            _logger.LogInformation("Getting book chapters ...");

            var getTocUrl = $"{book.Url}/muc-luc/page={0}";

            int page = 0;
            int number = 1;
            bool hasChapter = false;

            book.Chapters = new List<Chapter>();
            
            do
            {
                hasChapter = false;
                
                var bookChaptersRes = await _httpClient.Get(string.Format(getTocUrl, page.ToString()));
                var bookChaptersPage = _htmlParser.Parse(bookChaptersRes).DocumentNode;
                _logger.LogInformation($"Request book chapters page {page} success");
                
                var chapterElements = bookChaptersPage.QuerySelectorAll("#mucluc-list > div > ul > li > div.mucluc-chuong > a");

                hasChapter = chapterElements.Any();

                foreach (var chapterElement in chapterElements)
                {
                    var href = chapterElement.GetAttributeValue("href", string.Empty);
                    var title = chapterElement.InnerText;

                    title = title.Replace("&nbsp;", " ");
                    
                    book.Chapters.Add(new Chapter()
                    {
                        Number = number,
                        Name = title,
                        Url = $"{book.Url}{href}"
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
            
            var chapterContentElements = chapterPage.QuerySelectorAll("#noi-dung > p");
            var paragraphs = chapterContentElements.Select(x => x.InnerText);
            chapter.Paragraphs = paragraphs.ToList();
            _logger.LogInformation("Get chapter paragraphs success");
            
            _logger.LogInformation("Get chapter success");
        }
    }
}