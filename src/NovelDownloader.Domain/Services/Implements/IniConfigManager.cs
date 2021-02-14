using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements
{
    public class IniConfigManager: IConfigManager
    {
        private readonly ILogger<IniConfigManager> _logger;

        public IniConfigManager(ILogger<IniConfigManager> logger)
        {
            _logger = logger;
        }
        
        public async Task<List<BookStyle>> GetBookStyles(string configFile)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveBookStyles(List<BookStyle> bookStyles, string configFile)
        {
            throw new System.NotImplementedException();
        }
    }
}