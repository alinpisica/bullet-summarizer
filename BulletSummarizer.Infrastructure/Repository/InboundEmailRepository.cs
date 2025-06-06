using BulletSummarizer.Core.Models;
using BulletSummarizer.Infrastructure.Repository.Database.Queries;
using BulletSummarizer.Infrastructure.Repository.Interfaces;
using Dapper;
using System.Data;

namespace BulletSummarizer.Infrastructure.Repository
{
    public class InboundEmailRepository : IInboundEmailRepository
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public InboundEmailRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<InboundEmail>> GetUnprocessedInboundEmails(int count = 1, int maxRetryCount = 3, int retryIntervalInMinutes = 10)
        {
            return await _connection.QueryAsync<InboundEmail>(InboundEmailQueries.GetUnprocessedInboundEmails, new {
                Count = count,
                MaxRetryCount = maxRetryCount,
                RetryIntervalInMinutes = retryIntervalInMinutes
            }, _transaction);
        }

        public async Task<int> AddInboundEmail(InboundEmail inboundEmail)
        {
            return await _connection.ExecuteScalarAsync<int>(InboundEmailQueries.AddInboundEmail, inboundEmail, _transaction);
        }

        public async Task<int> UpdateQueueEntry(InboundEmail inboundEmail)
        {
            return await _connection.ExecuteScalarAsync<int>(InboundEmailQueries.UpdateQueueEntry, inboundEmail, _transaction);
        }

        public async Task<int> UpdateInboundEmail(InboundEmail inboundEmail)
        {
            return await _connection.ExecuteAsync(InboundEmailQueries.UpdateInboundEmail, inboundEmail, _transaction);
        }
    }
}
