using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Infrastructure.Services.Authorization
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
}
