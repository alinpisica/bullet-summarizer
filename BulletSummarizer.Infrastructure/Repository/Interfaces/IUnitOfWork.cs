namespace BulletSummarizer.Infrastructure.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IInboundEmailRepository InboundEmailRepository { get; }
        ISummarizationRepository SummarizationRepository { get; }

        void Commit();
    }
}
