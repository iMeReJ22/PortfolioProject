using MediatR;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public record GetActivityForBoardQuery (int boardId): IRequest<Unit>
    {
    }
}
