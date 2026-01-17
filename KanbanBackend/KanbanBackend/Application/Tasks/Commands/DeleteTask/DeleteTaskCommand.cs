using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(int Id) : IRequest<Unit>;

}
