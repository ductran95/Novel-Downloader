using System;
using System.IO;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Aggregators;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Extensions;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Domain.Services.Implements
{
    public class DefaultEbookFormatChecker: IEbookFormatChecker
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DefaultEbookFormatChecker> _logger;

        public DefaultEbookFormatChecker(IServiceProvider serviceProvider, ILogger<DefaultEbookFormatChecker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public Ebook GetEbook(string path)
        {
            _logger.LogInformation("Checking ebook file {path} ...", path);
            
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"File {path} not exists");
            }
            
            if (!File.Exists(path))
            {
                throw new ArgumentException($"File {path} not exists");
            }

            var ext = Path.GetExtension(path);
            ext = ext.TrimStart('.').ToLower();

            EbookFormatEnum format = 0;

            foreach (var ebookFormatEnum in Enum.GetValues<EbookFormatEnum>())
            {
                if (ebookFormatEnum.GetDisplayName() == ext)
                {
                    format = ebookFormatEnum;
                }
            }

            Ebook ebook;
            
            switch (format)
            {
                case EbookFormatEnum.Docx:
                    ebook = new DocxEbook()
                    {
                        FilePath = path,
                        IsExisted = true
                    };
                    break;
                
                case EbookFormatEnum.Epub:
                    ebook = new EpubEbook()
                    {
                        FilePath = path,
                        IsExisted = true
                    };
                    break;
                
                default:
                    throw new NotSupportedException("Only support Epub and DocX");
            }

            ebook.ServiceProvider = _serviceProvider;
            _logger.LogInformation("Check ebook file success");
            
            return ebook;
        }
        
        public Ebook GetEbook(string bookName, EbookFormatEnum format, string directory = null)
        {
            _logger.LogInformation("Checking ebook file with name {bookName} ...", bookName);
            
            var ext = format.GetDisplayName();
            var folder = directory ?? Directory.GetCurrentDirectory();
            var path = Path.Combine(folder, $"{bookName}.{ext}");

            Ebook ebook;
            
            switch (format)
            {
                case EbookFormatEnum.Docx:
                    ebook = new DocxEbook()
                    {
                        FilePath = path,
                        IsExisted = false
                    };
                    break;
                
                case EbookFormatEnum.Epub:
                    ebook = new EpubEbook()
                    {
                        FilePath = path,
                        IsExisted = false
                    };
                    break;
                
                default:
                    throw new NotSupportedException("Only support Epub and DocX");
            }
            
            _logger.LogInformation("Check ebook file success");

            return ebook;
        }
    }
}