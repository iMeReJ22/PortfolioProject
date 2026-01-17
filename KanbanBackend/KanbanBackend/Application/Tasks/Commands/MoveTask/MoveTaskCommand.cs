using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.MoveTask
{
    public record MoveTaskCommand(
    int TaskId,
    int TargetColumnId,
    int NewOrderIndex
) : IRequest<Unit>;

}
