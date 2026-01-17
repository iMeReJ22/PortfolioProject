using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.ReorderTasks
{
    public record ReorderTasksCommand(
    int ColumnId,
    IReadOnlyList<TaskOrderDto> Tasks
) : IRequest<Unit>;

    public record TaskOrderDto(int TaskId, int OrderIndex);

}
