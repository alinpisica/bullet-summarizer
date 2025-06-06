namespace BulletSummarizer.Infrastructure.Services.Interfaces
{
    public interface IPostmarkService
    {
        public Task<bool> SendEmail(string to, string subject, string htmlBody);
    }
}
