using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly KanbanDbContext _db;
        public ColumnRepository(KanbanDbContext db)
        {
            _db = db;
        }
        public async System.Threading.Tasks.Task AddAsync(Column column)
        {
            _db.Columns.Add(column);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Column column)
        {
            _db.Columns.Remove(column);
            await _db.SaveChangesAsync();
        }

        public async Task<Column?> GetByIdAsync(int id)
        {
            return await _db.Columns.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<Column>> GetForBoardAsync(int boardId)
        {
            return await _db.Columns.Where(c => c.BoardId == boardId)
                .ToListAsync();
        }

        public async Task<int> GetNextOrderIndexAsync(int boardId)
        {
            var maxOrder = await _db.Columns
                .Where(c => c.BoardId == boardId)
                .Select(c => c.OrderIndex)
                .DefaultIfEmpty(-1)
                .MaxAsync();
            return maxOrder + 1;
        }

        public async System.Threading.Tasks.Task ReorderAsync(int boardId, IReadOnlyCollection<Column> columns)
        {
            var boardColumns = await _db.Columns
                .Where(c => c.BoardId == boardId)
                .ToListAsync();

            foreach (var col in columns)
            {
                var match = boardColumns.FirstOrDefault(xc => xc.Id == col.Id);
                if (match != null)
                    match.OrderIndex = col.OrderIndex;
            }

            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Column column)
        {
            _db.Columns.Update(column);
            await _db.SaveChangesAsync();
        }
    }
}
