using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Constants;
using NovelDownloader.Domain.Extensions;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements
{
    public class DefaultStyleConfigManager: IConfigManager
    {
        private readonly ILogger<DefaultStyleConfigManager> _logger;
        private readonly List<BookStyle> _styles;

        public DefaultStyleConfigManager(ILogger<DefaultStyleConfigManager> logger)
        {
            _logger = logger;

            _styles = new List<BookStyle>()
            {
                new BookStyle()
                {
                    Name = BookStyleConstants.CoverImage,
                    Align = AlignEnum.Center,
                    Color = "black",
                    Height = 150,
                    Width = 150
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.Title,
                    Align = AlignEnum.Center,
                    Font = "Arial",
                    FontSize = 18,
                    FontStyle = FontStyleEnum.Bold,
                    Color = "black",
                    Margin = new List<int>{20, 0, 20, 0}
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.Metadata,
                    Align = AlignEnum.Center,
                    Font = "Arial",
                    FontSize = 16,
                    FontStyle = FontStyleEnum.Bold,
                    Color = "blue",
                    Margin = new List<int>{20, 0, 20, 0}
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.Description,
                    Align = AlignEnum.Left,
                    Font = "Arial",
                    FontSize = 14,
                    FontStyle = FontStyleEnum.Italic,
                    Color = "black",
                    Margin = new List<int>{10, 0, 10, 0}
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.Metadata,
                    Align = AlignEnum.Center,
                    Font = "Arial",
                    FontSize = 16,
                    FontStyle = FontStyleEnum.Bold,
                    Color = "blue",
                    Margin = new List<int>{20, 0, 20, 0}
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.TOCItem,
                    Align = AlignEnum.Left,
                    Font = "Arial",
                    FontSize = 14,
                    Color = "black",
                    Margin = new List<int>{10, 0, 10, 0}
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.ChapterHeader,
                    Align = AlignEnum.Center,
                    Font = "Arial",
                    FontSize = 16,
                    FontStyle = FontStyleEnum.Bold,
                    Color = "blue",
                    Margin = new List<int>{20, 0, 20, 0}
                },
                new BookStyle()
                {
                    Name = BookStyleConstants.ChapterContent,
                    Align = AlignEnum.Left,
                    Font = "Arial",
                    FontSize = 14,
                    Color = "black",
                    Margin = new List<int>{10, 0, 10, 0}
                },
            };
        }

        public Task<List<BookStyle>> GetBookStyles()
        {
            return Task.FromResult(_styles);
        }

        public Task<List<BookStyle>> GetBookStyles(string configFile)
        {
            _logger.LogWarning("DefaultStyleConfigManager get style from default path");
            return GetBookStyles();
        }

        public Task SaveBookStyles(List<BookStyle> bookStyles)
        {
            _logger.LogWarning("DefaultStyleConfigManager can not save style");
            return Task.CompletedTask;
        }

        public Task SaveBookStyles(List<BookStyle> bookStyles, string configFile)
        {
            _logger.LogWarning("DefaultStyleConfigManager save style to default path");
            return SaveBookStyles(bookStyles);
        }
    }
}