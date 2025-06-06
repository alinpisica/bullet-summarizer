using BulletSummarizer.Core.Models;
using BulletSummarizer.Infrastructure.Repository.Interfaces;
using BulletSummarizer.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BulletSummarizer.Infrastructure.Workers
{
    public class SummarizationProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SummarizationProcessor> _logger;

        public SummarizationProcessor(IServiceProvider serviceProvider, ILogger<SummarizationProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Summarization processing started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    IReasoningService reasoningService = scope.ServiceProvider.GetRequiredService<IReasoningService>();
                    IPostmarkService postmarkService = scope.ServiceProvider.GetRequiredService<IPostmarkService>();

                    if (QueuePollingProcessor.InboundEmailsQueue.Count == 0)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

                        continue;
                    }

                    InboundEmail inboundEmail = QueuePollingProcessor.InboundEmailsQueue.Dequeue();

                    _logger.LogInformation($"Retrieved from queue the inbound email with id {inboundEmail.Id}, retry count {inboundEmail.RetryCount}");

                    Summarization? summarization = reasoningService.SummarizeContent(inboundEmail.Content);

                    if (summarization == null)
                    {
                        _logger.LogWarning($"Summarization is null for inbound email with id {inboundEmail.Id} at retry count {inboundEmail.RetryCount}");

                        continue;
                    }

                    int summarizationId = await unitOfWork.SummarizationRepository.AddSummarization(summarization);

                    async Task AddEntitiesAsync<T>(IEnumerable<T>? entities, Func<T, Task> addFunc) where T : SummarizationEntityBase
                    {
                        if (entities == null)
                        {
                            return;
                        }

                        foreach (var entity in entities)
                        {
                            entity.SummarizationId = summarizationId;
                            await addFunc(entity);
                        }
                    }

                    await AddEntitiesAsync(summarization.Participants, unitOfWork.SummarizationRepository.AddParticipant);
                    await AddEntitiesAsync(summarization.ActionItems, unitOfWork.SummarizationRepository.AddActionItem);
                    await AddEntitiesAsync(summarization.Decisions, unitOfWork.SummarizationRepository.AddDecision);
                    await AddEntitiesAsync(summarization.KeyDatesAndDeadlines, unitOfWork.SummarizationRepository.AddKeyDate);
                    await AddEntitiesAsync(summarization.Bullets, unitOfWork.SummarizationRepository.AddBullet);

                    inboundEmail.SummarizationId = summarizationId;
                    await unitOfWork.InboundEmailRepository.UpdateInboundEmail(inboundEmail);

                    unitOfWork.Commit();

                    await postmarkService.SendEmail("contact@alinpisica.com", "Summarization", FormatSummarizationAsHtmlEmail(summarization));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing payloads.");
                }
            }

            _logger.LogInformation("Summarization processing stopped.");
        }

        private string FormatSummarizationAsHtmlEmail(Summarization summary)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<html><body>");
            sb.AppendLine("<h2>Email Summary</h2>");
            sb.AppendLine("<hr/>");

            sb.AppendLine($"<p><strong>High-Level Summary:</strong><br/>{summary.HighLevelSummary ?? "-"}</p>");
            sb.AppendLine($"<p><strong>Tone:</strong><br/>{summary.GeneralConversationalTone ?? "-"}</p>");

            if (summary.Participants?.Any() == true)
            {
                sb.AppendLine("<h3>Participants</h3><ul>");
                foreach (var p in summary.Participants)
                {
                    sb.AppendLine($"<li>{p.Name} ({p.Email}) - Role: {p.Role}</li>");
                }
                sb.AppendLine("</ul>");
            }

            if (summary.ActionItems?.Any() == true)
            {
                sb.AppendLine("<h3>Action Items</h3><ul>");
                foreach (var item in summary.ActionItems)
                {
                    sb.AppendLine($"<li><strong>{item.Title}</strong>: {item.Description}<br/>Assigned to: {item.Assignee}, Due: {item.DueDate}</li>");
                }
                sb.AppendLine("</ul>");
            }

            if (summary.Decisions?.Any() == true)
            {
                sb.AppendLine("<h3>Decisions</h3><ul>");
                foreach (var decision in summary.Decisions)
                {
                    sb.AppendLine($"<li>{decision.Content} (Decided by: {decision.DecidedBy}, Date: {decision.Date})</li>");
                }
                sb.AppendLine("</ul>");
            }

            if (summary.KeyDatesAndDeadlines?.Any() == true)
            {
                sb.AppendLine("<h3>Key Dates and Deadlines</h3><ul>");
                foreach (var date in summary.KeyDatesAndDeadlines)
                {
                    sb.AppendLine($"<li>{date.Label}: {date.Date}</li>");
                }
                sb.AppendLine("</ul>");
            }

            if (summary.Bullets?.Any() == true)
            {
                sb.AppendLine("<h3>Bullet Points</h3><ul>");
                foreach (var bullet in summary.Bullets)
                {
                    sb.AppendLine($"<li>{bullet.Content} (Related to: {bullet.RelatedTo})</li>");
                }
                sb.AppendLine("</ul>");
            }

            sb.AppendLine("</body></html>");
            return sb.ToString();
        }

    }
}
