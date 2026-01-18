using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface IColumnRepository
    {
        Task<Column?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Column>> GetForBoardAsync(int boardId);
        Task<int> GetNextOrderIndexAsync(int boardId);

        System.Threading.Tasks.Task AddAsync(Column column);
        System.Threading.Tasks.Task UpdateAsync(Column column);

        System.Threading.Tasks.Task DeleteAsync(Column column);
        System.Threading.Tasks.Task ReorderAsync(int boardId, IReadOnlyCollection<Column> columns);
        System.Threading.Tasks.Task<int> GetMaxId();

    }
}
