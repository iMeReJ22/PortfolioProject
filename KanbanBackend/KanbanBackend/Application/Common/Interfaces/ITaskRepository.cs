namespace KanbanBackend.Application.Common.Interfaces
{
    public interface ITaskRepository
    {
        Task<Domain.Entities.Task?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Domain.Entities.Task>> GetForColumnAsync(int column);
        Task<IReadOnlyCollection<Domain.Entities.Task>> GetForBoardAsync(int boardId);
        Task<int> GetNextOrderIndexAsync(int columnId);

        Task AddAsync(Domain.Entities.Task task);
        Task UpdateAsync(Domain.Entities.Task task);

        Task DeleteAsync(Domain.Entities.Task task);

        Task MoveAsync(Domain.Entities.Task task, int newColumnId, int newOrderIndex);
        Task ReorderAsync(int columnId, IReadOnlyCollection<Domain.Entities.Task> tasks);
        Task AssignTagAsync(int taskId, int tagId);
        Task RemoveTagAsync(int taskId, int tagId);
        System.Threading.Tasks.Task<int> GetMaxId();

    }
}
