using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners
{
    public record GetDashboardBoardsWithOwnersQuery(int UserId): IRequest<IReadOnlyList<BoardTileDto>>;
}
