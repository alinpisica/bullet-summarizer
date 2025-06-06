using BulletSummarizer.Core.Models;
using BulletSummarizer.Infrastructure.Repository.Database.Queries;
using BulletSummarizer.Infrastructure.Repository.Interfaces;
using Dapper;
using System.Data;

namespace BulletSummarizer.Infrastructure.Repository
{
    public class SummarizationRepository : ISummarizationRepository
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public SummarizationRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<int> AddSummarization(Summarization summarization)
        {
            return await _connection.ExecuteScalarAsync<int>(SummarizationQueries.AddSummarization, summarization, _transaction);
        }

        public async Task<int> AddActionItem(ActionItem actionItem)
        {
            return await _connection.ExecuteScalarAsync<int>(ActionItemQueries.AddActionItem, actionItem, _transaction);
        }

        public async Task<int> AddBullet(Bullet bullet)
        {
            return await _connection.ExecuteScalarAsync<int>(BulletQueries.AddBullet, bullet, _transaction);
        }

        public async Task<int> AddDecision(Decision decision)
        {
            return await _connection.ExecuteScalarAsync<int>(DecisionQueries.AddDecision, decision, _transaction);
        }

        public async Task<int> AddKeyDate(KeyDate keyDate)
        {
            return await _connection.ExecuteScalarAsync<int>(KeyDateQueries.AddKeyDate, keyDate, _transaction);
        }

        public async Task<int> AddParticipant(Participant participant)
        {
            return await _connection.ExecuteScalarAsync<int>(ParticipantQueries.AddParticipant, participant, _transaction);
        }
    }
}
