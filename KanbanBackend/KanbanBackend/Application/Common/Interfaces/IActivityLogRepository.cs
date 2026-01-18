using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface IActivityLogRepository
    {
        Task<IReadOnlyCollection<Domain.Entities.ActivityLog>> GetForBoardAsync(int boardId);
        Task<IReadOnlyCollection<Domain.Entities.ActivityLog>> GetForTaskAsync(int taskId);
        Task<IReadOnlyCollection<Domain.Entities.ActivityLog>> GetForTagAsync(int tagId);
        Task<IReadOnlyCollection<Domain.Entities.ActivityLog>> GetForColumnAsync(int columnId);
        Task<IReadOnlyCollection<Domain.Entities.ActivityLog>> GetForCommentAsync(int commentId);

        System.Threading.Tasks.Task AddAsync(Domain.Entities.ActivityLog activityLog);
        System.Threading.Tasks.Task AddRangeAsync(IEnumerable<Domain.Entities.ActivityLog> logs);

        System.Threading.Tasks.Task DeleteForBoardAsync(int boardId);
        System.Threading.Tasks.Task<int> GetMaxId();

    }
}
