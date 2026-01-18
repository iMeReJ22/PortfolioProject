using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly KanbanDbContext _db;
        public ActivityLogRepository(KanbanDbContext db)
        {
            _db = db;
        }
        public async System.Threading.Tasks.Task AddAsync(ActivityLog activityLog)
        {
            _db.ActivityLogs.Add(activityLog);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddRangeAsync(IEnumerable<ActivityLog> logs)
        {
            _db.ActivityLogs.AddRange(logs);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteForBoardAsync(int boardId)
        {
            var logs = await _db.ActivityLogs
                .Where(x => x.BoardId == boardId)
                .ToListAsync();

            _db.ActivityLogs.RemoveRange(logs);
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<ActivityLog>> GetForBoardAsync(int boardId)
        {
            var logs = await _db.ActivityLogs
                .Where(x => x.BoardId == boardId)
                .ToListAsync();
            return logs;
        }

        public async Task<IReadOnlyCollection<ActivityLog>> GetForColumnAsync(int columnId)
        {
            var logs = await _db.ActivityLogs
                .Where(x => x.ColumnId == columnId)
                .ToListAsync();
            return logs;
        }

        public async Task<IReadOnlyCollection<ActivityLog>> GetForCommentAsync(int commentId)
        {
            var logs = await _db.ActivityLogs
                .Where(x => x.TaskCommentId == commentId)
                .ToListAsync();
            return logs;
        }

        public async Task<IReadOnlyCollection<ActivityLog>> GetForTagAsync(int tagId)
        {
            var logs = await _db.ActivityLogs
                .Where(x => x.TagId == tagId)
                .ToListAsync();
            return logs;
        }

        public async Task<IReadOnlyCollection<ActivityLog>> GetForTaskAsync(int taskId)
        {
            var logs = await _db.ActivityLogs
                .Where(x => x.TaskId == taskId)
                .ToListAsync();
            return logs;
        }
        public async Task<int> GetMaxId()
        {
            return await _db.ActivityLogs.MaxAsync(x => (int?)x.Id) ?? 0;
        }
    }
}
