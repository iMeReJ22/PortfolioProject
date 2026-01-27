using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Domain.Entities;
using Async = System.Threading.Tasks;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface ITaskRepository
    {
        Task<Domain.Entities.Task?> GetByIdAsync(int id);
        Task<Domain.Entities.Task?> GetTaskAsync(int id);
        Task<IReadOnlyCollection<Domain.Entities.Task>> GetForColumnAsync(int column);
        Task<IReadOnlyCollection<Domain.Entities.Task>> GetForBoardAsync(int boardId);
        Task<int> GetNextOrderIndexAsync(int columnId);

        Async.Task AddAsync(Domain.Entities.Task task);
        Async.Task UpdateAsync(Domain.Entities.Task task);

        Async.Task DeleteAsync(Domain.Entities.Task task);
        Async.Task DeleteRangeAsync(IEnumerable<Domain.Entities.Task> task);
        Async.Task MoveAsync(Domain.Entities.Task task, int newColumnId, int newOrderIndex);
        Async.Task ReorderAsync(int columnId, IReadOnlyCollection<Domain.Entities.Task> tasks);
        Async.Task AssignTagAsync(int taskId, int tagId);
        Async.Task RemoveTagAsync(int taskId, int tagId);
        Task<int> GetMaxId();
        Task<IReadOnlyCollection<TaskType>> GetTaskTypesAsync();
    }
}
