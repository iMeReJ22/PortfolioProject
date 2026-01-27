using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Application.Tasks.Commands.ReorderTasks;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly KanbanDbContext _db;
        public TaskRepository(KanbanDbContext db)
        {
            _db = db;
        }
        public async System.Threading.Tasks.Task AddAsync(Domain.Entities.Task task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AssignTagAsync(int taskId, int tagId)
        {
            var tag = await _db.Tags.FirstAsync(t => t.Id == tagId);

            var task = await _db.Tasks
                .Include(t => t.Tags)
                .FirstAsync(t => t.Id == taskId);

            task.Tags.Add(tag);

            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Domain.Entities.Task task)
        {
            task.Tags.Clear();
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        public async Task<Domain.Entities.Task?> GetByIdAsync(int id)
        {
            return await _db.Tasks.FirstAsync(t=> t.Id == id);
        }

        public async Task<IReadOnlyCollection<Domain.Entities.Task>> GetForBoardAsync(int boardId)
        {
            return await _db.Tasks
                .Where(t => t.Column.BoardId == boardId)
                .OrderBy(t => t.Column.BoardId)
                .ThenBy(t => t.OrderIndex)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Domain.Entities.Task>> GetForColumnAsync(int column)
        {
            return await _db.Tasks
                .Where(t => t.ColumnId == column)
                .OrderBy(t => t.OrderIndex)
                .ToListAsync();
        }

        public async Task<int> GetNextOrderIndexAsync(int columnId)
        {
            var maxOrder = await _db.Tasks
                .Where(t => t.ColumnId == columnId)
                .MaxAsync(t => (int?)t.OrderIndex) ?? 0;

            return maxOrder + 1;
        }

        public async System.Threading.Tasks.Task MoveAsync(Domain.Entities.Task task, int newColumnId, int newOrderIndex)
        {

            if (task.ColumnId != newColumnId){
                var oldColumnTasks = await _db.Tasks
                    .Where(t => t.ColumnId == task.ColumnId && t.Id != task.Id)
                    .OrderBy(t => t.OrderIndex)
                    .ToListAsync();

                ReorderTasks(oldColumnTasks);
            }

            var newColumnTasks = await _db.Tasks
                .Where(t => t.ColumnId == newColumnId && t.Id != task.Id)
                .OrderBy(t => t.OrderIndex)
                .ToListAsync();

            if(task.ColumnId != newColumnId)
                task.ColumnId = newColumnId;
            
            newColumnTasks.Insert(newOrderIndex-1, task);
            ReorderTasks(newColumnTasks);

            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task RemoveTagAsync(int taskId, int tagId)
        {
            var task = await _db.Tasks
                .Include(t => t.Tags)
                .FirstAsync(t => t.Id == taskId);
            var tag = await _db.Tags
                .FirstAsync(t => t.Id == tagId);
            task.Tags.Remove(tag);

            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task ReorderAsync(int columnId, IReadOnlyCollection<Domain.Entities.Task> tasks)
        {
            var columnTasks = await _db.Tasks
                .Where(t => t.ColumnId == columnId)
                .ToListAsync();

            foreach (var task in tasks)
            {
                var match = columnTasks.FirstOrDefault(t => t.Id == task.Id);
                if (match != null)
                    match.OrderIndex = task.OrderIndex;
            }

            await _db.SaveChangesAsync();
        }

        private void ReorderTasks(IList<Domain.Entities.Task> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].OrderIndex = i+1;
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync(Domain.Entities.Task task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetMaxId()
        {
            return await _db.Tasks.MaxAsync(t => (int?)t.Id) ?? 0;
        }

        public async System.Threading.Tasks.Task DeleteRangeAsync(IEnumerable<Domain.Entities.Task> task)
        {
            _db.Tasks.RemoveRange(task);
            await _db.SaveChangesAsync();
        }

        public async Task<Domain.Entities.Task?> GetTaskAsync(int id)
        {
            return await _db.Tasks
                .Include(t => t.Tags)
                .Include(t => t.Column)
                .Include(t => t.TaskType)
                .Include(t => t.CreatedByUser)
                .Include(t => t.ActivityLogs)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<IReadOnlyCollection<TaskType>> GetTaskTypesAsync()
        {
            return await _db.TaskTypes
                .ToListAsync();
        }
    }
}
