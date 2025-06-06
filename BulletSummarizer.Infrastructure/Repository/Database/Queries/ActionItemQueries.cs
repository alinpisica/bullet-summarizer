namespace BulletSummarizer.Infrastructure.Repository.Database.Queries
{
    public class ActionItemQueries
    {
        public static string AddActionItem 
            = @"INSERT INTO action_item 
                        (summarization_id, title, description, assignee, due_date) 
                VALUES (@SummarizationId, @Title, @Description, @Assignee, @DueDate);";
    }
}
