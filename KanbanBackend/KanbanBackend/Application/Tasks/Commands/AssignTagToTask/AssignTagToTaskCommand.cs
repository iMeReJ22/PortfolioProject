using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.AssignTagToTask
{
    public record AssignTagToTaskCommand(
    int TaskId,
    int TagId
) : IRequest<Unit>;

}
