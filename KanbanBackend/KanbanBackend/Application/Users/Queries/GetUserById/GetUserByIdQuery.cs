using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int userId) : IRequest<UserDto>
    {
    }
}
