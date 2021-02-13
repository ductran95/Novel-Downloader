using HtmlAgilityPack;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IHtmlParser
    {
        HtmlDocument Parse(string data);
    }
}