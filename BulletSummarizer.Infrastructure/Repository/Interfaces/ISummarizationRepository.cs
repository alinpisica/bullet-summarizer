using BulletSummarizer.Core.Models;

namespace BulletSummarizer.Infrastructure.Repository.Interfaces
{
    public interface ISummarizationRepository
    {
        public Task<int> AddSummarization(Summarization summarization);

        public Task<int> AddActionItem(ActionItem actionItem);

        public Task<int> AddBullet(Bullet bullet);

        public Task<int> AddDecision(Decision decision);

        public Task<int> AddKeyDate(KeyDate keyDate);

        public Task<int> AddParticipant(Participant participant);
    }
}
