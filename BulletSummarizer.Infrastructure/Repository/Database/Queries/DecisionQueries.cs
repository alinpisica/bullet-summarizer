namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class DecisionQueries
    {
        public static string AddDecision 
            = @"INSERT INTO decision 
                        (summarization_id, content, decided_by, date) 
                VALUES (@SummarizationId, @Content, @DecidedBy, @Date);";
    }
}
