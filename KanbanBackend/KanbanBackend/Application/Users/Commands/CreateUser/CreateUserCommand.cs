using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
    string Email,
    string DisplayName,
    string Password
) : IRequest<UserDto>;


}
