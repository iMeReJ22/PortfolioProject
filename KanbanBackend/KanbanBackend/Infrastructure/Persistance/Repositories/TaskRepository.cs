using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Application.Tasks.Commands.ReorderTasks;
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
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Domain.Entities.Task>> GetForColumnAsync(int column)
        {
            return await _db.Tasks
                .Where(t => t.ColumnId == column)
                .ToListAsync();
        }

        public async Task<int> GetNextOrderIndexAsync(int columnId)
        {
            var maxOrder = await _db.Tasks
                .Where(t => t.ColumnId == columnId)
                .Select(t => t.OrderIndex)
                .DefaultIfEmpty(-1)
                .MaxAsync();

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
            
            newColumnTasks.Insert(newOrderIndex, task);
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
                tasks[i].OrderIndex = i;
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync(Domain.Entities.Task task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }
    }
}
