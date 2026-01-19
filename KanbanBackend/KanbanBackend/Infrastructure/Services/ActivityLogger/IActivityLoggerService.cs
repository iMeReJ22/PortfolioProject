namespace KanbanBackend.Infrastructure.Services.ActivityLogger
{
    public interface IActivityLoggerService
    {
        Task AddLogBoardAsync(string name, string description, int boardId);
        Task AddLogBoardMemberAsync(string name, string description, int boardId, int userId);
        Task AddLogTagAsync(string name, string description, int tagId);
        Task AddLogColumnAsync(string name, string description, int columnId);
        Task AddLogTaskAsync(string name, string description, int taskId);
        Task AddLogTagAssignAsync(string name, string description, int tagId, int taskId);
        Task AddLogCommentAsync(string name, string description, int commentId);
    }
}
