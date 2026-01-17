using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Tag>> GetForBoardAsync(int boardId);
        Task<IReadOnlyList<Tag>> GetForTaskAsync(int taskId);

        System.Threading.Tasks.Task AddAsync(Tag tag);
        System.Threading.Tasks.Task UpdateAsync(Tag tag);

        System.Threading.Tasks.Task DeleteAsync(Tag Tag);
        System.Threading.Tasks.Task RemoveTagFromTaskAsync(int taskId, int tagId);
        System.Threading.Tasks.Task RemoveAllTagsFromTaskAsync(int taskId);
        System.Threading.Tasks.Task RemoveAllTagsForBoardAsync(int boardId);
    }
}
