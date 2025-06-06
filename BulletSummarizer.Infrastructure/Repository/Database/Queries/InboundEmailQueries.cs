namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class InboundEmailQueries
    {
        public static readonly string AddInboundEmail = "INSERT INTO inbound_email (content) VALUES (@Content);";

        public static readonly string GetUnprocessedInboundEmails = @"
            SELECT 
                id AS ""Id"",
                content ""Content"",
                summarization_id AS ""SummarizationId"",
                retry_count AS ""RetryCount""
            FROM inbound_email 
            WHERE summarization_id IS NULL 
            AND (retry_count IS NULL OR retry_count < @MaxRetryCount)
            AND (queue_entry_time IS NULL OR (NOW() - queue_entry_time > (@RetryIntervalInMinutes || ' minutes')::INTERVAL))";

        public static readonly string UpdateQueueEntry = @"
            UPDATE inbound_email
            SET retry_count = retry_count + 1,
                queue_entry_time = NOW()
            WHERE id = @Id";

        public static readonly string UpdateInboundEmail = @"
            UPDATE inbound_email
            SET summarization_id = @SummarizationId
            WHERE id = @Id";
    }
}
