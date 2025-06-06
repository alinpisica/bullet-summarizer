namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class SummarizationQueries
    {
        public static string AddSummarization
            = @"INSERT INTO summarization 
                        (inbound_email_id, high_level_summary, general_conversational_tone) 
                VALUES (@InboundEmailId, @HighLevelSummary, @GeneralConversationalTone)
                RETURNING id;";
    }
}
