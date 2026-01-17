using MediatR;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public record GetActivityForBoardCommand (int boardId): IRequest<Unit>
    {
    }
}
