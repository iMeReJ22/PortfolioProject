using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetUserAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);

        System.Threading.Tasks.Task AddAsync(User user);
        System.Threading.Tasks.Task UpdateAsync(User user);
        System.Threading.Tasks.Task<ICollection<User>> GetUsers();
        Task<int> GetMaxId();
        System.Threading.Tasks.Task DeleteAsync(User user);
    }
}
