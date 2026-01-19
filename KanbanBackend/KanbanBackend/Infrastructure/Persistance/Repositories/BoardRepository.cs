using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly KanbanDbContext _db;

        public BoardRepository(KanbanDbContext db)
        {
            _db = db;
        }
        public async System.Threading.Tasks.Task AddAsync(Board board)
        {
            _db.Boards.Add(board);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddMemberAsync(BoardMember member)
        {
            _db.BoardMembers.Add(member);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Board board)
        {
            _db.Boards.Remove(board);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var board = await _db.Boards.FindAsync(id);
            return board != null;
        }

        public async Task<Board?> GetBoardAsync(int id)
        {
            return await _db.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Tasks)
                .Include(b => b.BoardMembers)
                .ThenInclude(bm => bm.User)
                .Include(b => b.Tags)
                .Include(b => b.ActivityLogs)
                .FirstOrDefaultAsync(b  => b.Id == id);
        }

        public async Task<Board?> GetByIdAsync(int id)
        {
            return await _db.Boards.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<Board>> GetForUserAsync(int userId)
        {
            return await _db.Boards
                .Where(b => b.BoardMembers.Any(bm => bm.UserId == userId))
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<BoardMember>> GetMembersAsync(int boardId)
        {
            return await _db.BoardMembers
                .Include(bm => bm.User)
                .Where(bm => bm.BoardId == boardId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task RemoveMemberAsync(BoardMember member)
        {
            _db.BoardMembers.Remove(member);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Board board)
        {
            _db.Boards.Update(board);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> UserHasAccessAsync(int boardId, int userId)
        {
            var boardMember = await _db.BoardMembers
                .Where(bm => bm.UserId == userId &&  bm.BoardId == boardId)
                .FirstOrDefaultAsync();
            return boardMember != null;
        }
        public async Task<int> GetMaxId()
        {
            return await _db.Boards.MaxAsync(b => (int?)b.Id) ?? 0;
        }

        public async Task<BoardMember?> GetMemberAsync(int boardId, int memberId)
        {
            return await _db.BoardMembers
                .Include(bm => bm.Board)
                .Include(bm => bm.User)
                .FirstOrDefaultAsync(bm => bm.BoardId == boardId && bm.UserId == memberId);
        }
    }
}
