using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.RemoveTagFromTask
{
    public record RemoveTagFromTaskCommand(
    int TaskId,
    int TagId
) : IRequest<Unit>;

}
