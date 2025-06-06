using BulletSummarizer.Infrastructure.Repository.Interfaces;
using System.Data;

namespace BulletSummarizer.Infrastructure.Repository
{
    public class UnitOfWork(
       IDbTransaction transaction,

        IInboundEmailRepository inboundEmailRepository,
        ISummarizationRepository summarizationRepository
   ) : IUnitOfWork, IDisposable
    {
        public IInboundEmailRepository InboundEmailRepository { get; } = inboundEmailRepository;
        public ISummarizationRepository SummarizationRepository { get; } = summarizationRepository;


        private IDbTransaction? _transaction = transaction;

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
            }
        }

        public void Dispose()
        {
            _transaction?.Connection?.Close();
            _transaction?.Connection?.Dispose();
            _transaction?.Dispose();
        }
    }
}
