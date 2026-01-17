using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetBoardsForUser
{
    public record GetBoardsForUserQuery(int UserId) : IRequest<IReadOnlyList<BoardDto>>;

}
