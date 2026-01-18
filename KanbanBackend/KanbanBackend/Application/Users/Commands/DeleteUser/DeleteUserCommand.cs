using MediatR;

namespace KanbanBackend.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int id) : IRequest<Unit>
    {
    }
}
