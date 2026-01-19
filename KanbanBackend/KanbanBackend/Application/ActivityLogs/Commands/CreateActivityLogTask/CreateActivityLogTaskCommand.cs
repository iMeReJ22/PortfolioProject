using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.ActivityLog.Commands.CreateActivityLogTask
{
    public record CreateActivityLogTaskCommand(
        int BoardId,
        int? TaskId,
        int UserId,
        string? Name,
        string? Description
    ) : IRequest<ActivityLogDto>;


}
