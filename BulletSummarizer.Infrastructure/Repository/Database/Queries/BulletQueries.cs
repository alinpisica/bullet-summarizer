namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class BulletQueries
    {
        public static string AddBullet 
            = @"INSERT INTO bullet 
                        (summarization_id, content, related_to) 
                VALUES (@SummarizationId, @Content, @RelatedTo);";
    }
}
