namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class ParticipantQueries
    {
        public static string AddParticipant
            = @"INSERT INTO participant 
                        (summarization_id, name, email, role) 
                VALUES (@SummarizationId, @Name, @Email, @Role);";
    }
}
