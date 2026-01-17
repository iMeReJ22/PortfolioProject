using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.Login
{
    public record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginResultDto>;

}
