using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task<TaskComment?> GetByIdAsync(int id);
        Task<TaskComment?> GetCommentIdAsync(int id);
        Task<IReadOnlyCollection<TaskComment>> GetForTaskAsync(int taskId);

        System.Threading.Tasks.Task AddAsync(TaskComment comment);
        System.Threading.Tasks.Task UpdateAsync(TaskComment comment);

        System.Threading.Tasks.Task DeleteAsync(TaskComment comment);
        System.Threading.Tasks.Task DeleteRangeAsync(IEnumerable<TaskComment> comment);
        System.Threading.Tasks.Task DeleteForTaskAsync(int taskId);
        System.Threading.Tasks.Task<int> GetMaxId();

    }
}
