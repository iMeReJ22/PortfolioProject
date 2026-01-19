using ActivityLogEntity = KanbanBackend.Domain.Entities.ActivityLog;
namespace KanbanBackend.Application.Common.Interfaces
{
    public interface IActivityLogRepository
    {
        Task<IReadOnlyCollection<ActivityLogEntity>> GetForBoardAsync(int boardId);
        Task<IReadOnlyCollection<ActivityLogEntity>> GetForTaskAsync(int taskId);
        Task<IReadOnlyCollection<ActivityLogEntity>> GetForTagAsync(int tagId);
        Task<IReadOnlyCollection<ActivityLogEntity>> GetForColumnAsync(int columnId);
        Task<IReadOnlyCollection<ActivityLogEntity>> GetForCommentAsync(int commentId);
        System.Threading.Tasks.Task AddAsync(ActivityLogEntity activityLog);
        System.Threading.Tasks.Task AddRangeAsync(IEnumerable<ActivityLogEntity> logs);

        System.Threading.Tasks.Task DeleteForBoardAsync(int boardId);
        System.Threading.Tasks.Task DeleteRangeAsync(IEnumerable<ActivityLogEntity> logs);
        System.Threading.Tasks.Task<int> GetMaxId();

    }
}
