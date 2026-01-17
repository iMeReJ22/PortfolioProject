using MediatR;

namespace KanbanBackend.Application.Tags.Commands.DeleteTag
{
    public record DeleteTagCommand(int Id) : IRequest<Unit>;

}
