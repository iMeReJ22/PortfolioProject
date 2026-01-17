using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task<TaskComment?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<TaskComment>> GetForTaskAsync(int taskId);

        System.Threading.Tasks.Task AddAsync(TaskComment comment);
        System.Threading.Tasks.Task UpdateAsync(TaskComment comment);

        System.Threading.Tasks.Task DeleteAsync(TaskComment comment);
        System.Threading.Tasks.Task DeleteForTaskAsync(int taskId);
    }
}
