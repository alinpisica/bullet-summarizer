using BulletSummarizer.Core.Models;

namespace BulletSummarizer.Infrastructure.Repository.Interfaces
{
    public interface IInboundEmailRepository
    {
        public Task<IEnumerable<InboundEmail>> GetUnprocessedInboundEmails(int count = 1, int maxRetryCount = 3, int retryIntervalInMinutes = 10);

        public Task<int> AddInboundEmail(InboundEmail inboundEmail);

        public Task<int> UpdateQueueEntry(InboundEmail inboundEmail);

        public Task<int> UpdateInboundEmail(InboundEmail inboundEmail);
    }
}
