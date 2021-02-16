using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NovelDownloader.Domain.Enums;
using NovelDownloader.Domain.Services.Abstractions;

namespace NovelDownloader.Console
{
    public class TestWorker: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TestWorker> _logger;
        private readonly IBookUrlChecker _urlChecker;

        public TestWorker(IServiceProvider serviceProvider, ILogger<TestWorker> logger, IBookUrlChecker urlChecker)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _urlChecker = urlChecker;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ...");

            var url = @"https://truyen.tangthuvien.vn/doc-truyen/nga-dich-tri-du-he-du-hi";
            var format = EbookFormatEnum.Docx;

            var book = _urlChecker.CreateBook(url);
            await book.CreateNew(_serviceProvider, format, cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ...");
        }
    }
}