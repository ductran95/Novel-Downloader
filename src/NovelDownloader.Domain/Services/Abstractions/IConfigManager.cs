using System.Collections.Generic;
using System.Threading.Tasks;
using NovelDownloader.Domain.Aggregators;

namespace NovelDownloader.Domain.Services.Abstractions
{
    public interface IConfigManager
    {
        Task<List<BookStyle>> GetBookStyles();
        Task<List<BookStyle>> GetBookStyles(string configFile);
        Task SaveBookStyles(List<BookStyle> bookStyles);
        Task SaveBookStyles(List<BookStyle> bookStyles, string configFile);
    }
}