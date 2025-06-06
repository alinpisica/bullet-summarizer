using BulletSummarizer.Core.Models;

namespace BulletSummarizer.Infrastructure.Services.Interfaces
{
    public interface IReasoningService
    {
        public Summarization? SummarizeContent(string content);
    }
}
