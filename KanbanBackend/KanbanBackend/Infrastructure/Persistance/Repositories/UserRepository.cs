using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KanbanDbContext _db;
        public UserRepository(KanbanDbContext db)
        {
            _db = db;
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await GetByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var user = await GetByIdAsync(id);
            return user != null;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> GetMaxId()
        {
            return await _db.Users.MaxAsync(u => (int?)u.Id) ?? 0;
        }

        public async System.Threading.Tasks.Task<ICollection<User>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _db.Users
                .Include(u => u.BoardMembers)
                .ThenInclude(bm => bm.Board)
                .Include(u => u.Boards)
                .Include(u => u.ActivityLogMembers)
                .Include(u => u.ActivityLogActivityAuthors)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
