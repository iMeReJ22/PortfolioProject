using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetBoardMembers
{
    public record GetBoardMembersQuery(int BoardId) : IRequest<BoardMembersDto>;
}
