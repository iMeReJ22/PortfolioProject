using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        int Id,
        string Email,
        string DisplayName,
        string Password
    ) : IRequest<UserDto>;
}
