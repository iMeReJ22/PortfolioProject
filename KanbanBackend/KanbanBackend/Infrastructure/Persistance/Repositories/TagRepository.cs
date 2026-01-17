using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SystemTasks = System.Threading.Tasks;
namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly KanbanDbContext _db;
        public TagRepository(KanbanDbContext db)
        {
            _db = db;
        }

        public async SystemTasks.Task AddAsync(Tag tag)
        {
            _db.Tags.Add(tag);
            await _db.SaveChangesAsync();
        }

        public async SystemTasks.Task DeleteAsync(Tag Tag)
        {
            _db.Tags.Remove(Tag);
            await _db.SaveChangesAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _db.Tags.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<Tag>> GetForBoardAsync(int boardId)
        {
            return await _db.Tags
                .Where(t => t.BoardId == boardId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Tag>> GetForTaskAsync(int taskId)
        {
            return await _db.Tags
                .Include(t => t.Tasks.Any(task => task.Id == taskId))
                .ToListAsync();
        }

        public async SystemTasks.Task RemoveAllTagsForBoardAsync(int boardId)
        {
            var boardTags = await _db.Tags
                .Where(t => t.BoardId == boardId)
                .ToListAsync();

            _db.Tags.RemoveRange(boardTags);
            await _db.SaveChangesAsync();
        }

        public async SystemTasks.Task RemoveAllTagsFromTaskAsync(int taskId)
        {
            var task = await _db.Tasks
                .Include(t => t.Tags)
                .FirstAsync(t => t.Id == taskId);

            task.Tags.Clear();

            await _db.SaveChangesAsync();
        }

        public async SystemTasks.Task RemoveTagFromTaskAsync(int taskId, int tagId)
        {
            var task = await _db.Tasks
                .Include(t => t.Tags)
                .FirstAsync(t => t.Id == taskId);
            var tag = await _db.Tags
                .FirstAsync(t => t.Id == tagId);
            task.Tags.Remove(tag);

            await _db.SaveChangesAsync();
        }

        public async SystemTasks.Task UpdateAsync(Tag tag)
        {
            _db.Tags.Update(tag);
            await _db.SaveChangesAsync();
        }
    }
}
