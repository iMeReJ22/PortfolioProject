using MediatR;

namespace KanbanBackend.Application.Columns.Commands.DeleteColumn
{
    public record DeleteColumnCommand(int Id) : IRequest<Unit>;
}
