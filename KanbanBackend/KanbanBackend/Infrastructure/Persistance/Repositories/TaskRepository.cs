using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Application.Tasks.Commands.ReorderTasks;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AsyncTask = System.Threading.Tasks.Task;
using Task = KanbanBackend.Domain.Entities.Task;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly KanbanDbContext _db;
        public TaskRepository(KanbanDbContext db)
        {
            _db = db;
        }
        public async AsyncTask AddAsync(Task task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public async AsyncTask AssignTagAsync(int taskId, int tagId)
        {
            var tag = await _db.Tags.FirstAsync(t => t.Id == tagId);

            var task = await _db.Tasks
                .Include(t => t.Tags)
                .FirstAsync(t => t.Id == taskId);

            task.Tags.Add(tag);

            await _db.SaveChangesAsync();
        }

        public async AsyncTask DeleteAsync(Task task)
        {
            task.Tags.Clear();
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        public async Task<Task?> GetByIdAsync(int id)
        {
            return await _db.Tasks.FirstAsync(t=> t.Id == id);
        }

        public async Task<IReadOnlyCollection<Task>> GetForBoardAsync(int boardId)
        {
            return await _db.Tasks
                .Where(t => t.Column.BoardId == boardId)
                .OrderBy(t => t.Column.BoardId)
                .ThenBy(t => t.OrderIndex)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Task>> GetForColumnAsync(int column)
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

        public async AsyncTask MoveAsync(Task task, int newColumnId, int newOrderIndex)
        {
            if(task.ColumnId == newColumnId)
            {
                var columnTasks = await _db.Tasks
                    .Where(t => t.ColumnId == task.ColumnId)
                    .OrderBy(t => t.OrderIndex)
                    .ToListAsync();

                var toUpdateTasks = MoveSameColumn(columnTasks, newOrderIndex, task);

                _db.Tasks.UpdateRange(toUpdateTasks);
            } else
            {
                var oldColumnTasks = await _db.Tasks
                    .Where(t => t.ColumnId == task.ColumnId)
                    .OrderBy(t => t.OrderIndex)
                    .ToListAsync();

                var newColumnTasks = await _db.Tasks
                    .Where(t => t.ColumnId == newColumnId)
                    .OrderBy(t => t.OrderIndex)
                    .ToListAsync();

                task.ColumnId = newColumnId;

                var toUpdateTasks = MoveDifferntColumns(oldColumnTasks, newColumnTasks, newOrderIndex, task);

                _db.Tasks.UpdateRange(toUpdateTasks);
            }

            await _db.SaveChangesAsync();
        }

        private  IList<Task> MoveSameColumn(IList<Task> columnTasks, int newOrderIndex, Task task)
        {
            columnTasks.Remove(task);
            columnTasks.Insert(newOrderIndex, task);
            ReorderTasks(columnTasks);

            return columnTasks;
        }

        private IList<Task> MoveDifferntColumns(IList<Task> oldColumnTasks, IList<Task> newColumnTasks, int newOrderIndex, Task task)
        {
            oldColumnTasks.Remove(task);
            ReorderTasks(oldColumnTasks);

            newColumnTasks.Insert(newOrderIndex, task);
            ReorderTasks(newColumnTasks);

            oldColumnTasks.Concat(newColumnTasks);

            return oldColumnTasks;
        }

        public async AsyncTask RemoveTagAsync(int taskId, int tagId)
        {
            var task = await _db.Tasks
                .Include(t => t.Tags)
                .FirstAsync(t => t.Id == taskId);
            var tag = await _db.Tags
                .FirstAsync(t => t.Id == tagId);
            task.Tags.Remove(tag);

            await _db.SaveChangesAsync();
        }

        public async AsyncTask ReorderAsync(int columnId, IReadOnlyCollection<Task> tasks)
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

        private void ReorderTasks(IList<Task> tasks)
        {
            int i = 0;
            foreach (var task in tasks)
            {
                task.OrderIndex = i++;
            }
        }

        public async AsyncTask UpdateAsync(Task task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetMaxId()
        {
            return await _db.Tasks.MaxAsync(t => (int?)t.Id) ?? 0;
        }

        public async AsyncTask DeleteRangeAsync(IEnumerable<Task> task)
        {
            _db.Tasks.RemoveRange(task);
            await _db.SaveChangesAsync();
        }

        public async Task<Task?> GetTaskAsync(int id)
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
