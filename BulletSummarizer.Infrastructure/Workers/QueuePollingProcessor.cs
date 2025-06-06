using BulletSummarizer.Core.Models;
using BulletSummarizer.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BulletSummarizer.Infrastructure.Workers
{
    public class QueuePollingProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<QueuePollingProcessor> _logger;

        public static Queue<InboundEmail> InboundEmailsQueue = new();

        public QueuePollingProcessor(IServiceProvider serviceProvider, ILogger<QueuePollingProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queue polling processing started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var unprocessedEmails = await unitOfWork.InboundEmailRepository.GetUnprocessedInboundEmails();

                    if (!unprocessedEmails.Any())
                    {
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                        continue;
                    }

                    _logger.LogInformation($"Retrieved {unprocessedEmails.Count()} unprocessed emails: {string.Join(", ", unprocessedEmails.Select(email => $"(Id: {email.Id}, RetryCount: {email.RetryCount})"))}");

                    foreach (InboundEmail unprocessedEmail in unprocessedEmails)
                    {
                        InboundEmailsQueue.Enqueue(unprocessedEmail);

                        await unitOfWork.InboundEmailRepository.UpdateQueueEntry(unprocessedEmail);
                    }

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing payloads.");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("Summarization processing stopped.");
        }

    }
}
