namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class KeyDateQueries
    {
        public static string AddKeyDate
            = @"INSERT INTO key_date 
                        (summarization_id, label, date) 
                VALUES (@SummarizationId, @Label, @Date);";
    }
}
