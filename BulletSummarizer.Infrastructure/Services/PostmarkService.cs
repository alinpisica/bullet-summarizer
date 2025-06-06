using Azure;
using PostmarkDotNet.Model;
using PostmarkDotNet;
using BulletSummarizer.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BulletSummarizer.Infrastructure.Services
{
    public class PostmarkService : IPostmarkService
    {
        private readonly ILogger<PostmarkService> _logger;

        private readonly string? _postmarkServerToken;
        private readonly string? _postmarkMailFrom;

        private readonly PostmarkClient _postmarkClient;

        public PostmarkService(ILogger<PostmarkService> logger, IConfiguration configuration)
        {
            _logger = logger;

            IConfigurationSection postmarkConfiguration = configuration.GetSection("Postmark");

            _postmarkServerToken = postmarkConfiguration["ServerToken"];
            _postmarkMailFrom = postmarkConfiguration["MailFrom"];

            if (string.IsNullOrWhiteSpace(_postmarkServerToken))
            {
                _logger.LogError("Postmark server token missing from configuration file");
            }

            if (string.IsNullOrWhiteSpace(_postmarkMailFrom))
            {
                _logger.LogError("Postmark mail from missing from configuration file");
            }

            _postmarkClient = new(_postmarkServerToken);
        }

        public async Task<bool> SendEmail(string to, string subject, string htmlBody)
        {
            PostmarkMessage message = new()
            {
                To = to,
                From = _postmarkMailFrom,
                Subject = subject,
                HtmlBody = htmlBody
            };

            PostmarkResponse sendResult = await _postmarkClient.SendMessageAsync(message);

            _logger.LogInformation($"Email sending result: <{JsonSerializer.Serialize(sendResult)}>");

            return sendResult.Status == PostmarkStatus.Success;
        }
    }
}
