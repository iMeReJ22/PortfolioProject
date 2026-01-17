using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int userId) : IRequest<Unit>
    {
    }
}
