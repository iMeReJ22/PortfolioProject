using MediatR;

namespace KanbanBackend.Application.ActivityLog.Commands.CreateActivityLogTask
{
    public record CreateActivityLogTaskCommand(
        int BoardId,
        int? TaskId,
        int UserId,
        int ActionId,
        string? Description
    ) : IRequest<Unit>;


}
