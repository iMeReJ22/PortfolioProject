using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUsersByBoard
{
    public record GetUsersByBoardQuery(int boardId) : IRequest<UserDto[]>
    {
    }
}
