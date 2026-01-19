using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly KanbanDbContext _db;
        public TaskCommentRepository(KanbanDbContext db)
        {
            _db = db;
        }
        public async System.Threading.Tasks.Task AddAsync(TaskComment comment)
        {
            _db.TaskComments.Add(comment);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(TaskComment comment)
        {
            _db.TaskComments.Remove(comment);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteForTaskAsync(int taskId)
        {
            var comments = await GetForTaskAsync(taskId);

            _db.TaskComments.RemoveRange(comments);
            await _db.SaveChangesAsync();
        }

        public async Task<TaskComment?> GetByIdAsync(int id)
        {
            return await _db.TaskComments.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<TaskComment>> GetForTaskAsync(int taskId)
        {
            return await _db.TaskComments
                .Where(c => c.TaskId == taskId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(TaskComment comment)
        {
            _db.TaskComments.Update(comment);
            await _db.SaveChangesAsync();
        }
        public async Task<int> GetMaxId()
        {
            return await _db.TaskComments.MaxAsync(c => (int?)c.Id) ?? 0;
        }

        public async System.Threading.Tasks.Task DeleteRangeAsync(IEnumerable<TaskComment> comment)
        {
            _db.TaskComments.RemoveRange(comment);
            await _db.SaveChangesAsync(); ;
        }

        public async Task<TaskComment?> GetCommentIdAsync(int id)
        {
            return await _db.TaskComments
                .Include(tc => tc.Task)
                .ThenInclude(t => t.Column)
                .Include(tc => tc.Author)
                .Include(tc => tc.ActivityLogs)
                .FirstOrDefaultAsync(tc =>  tc.Id == id);
        }
    }
}
