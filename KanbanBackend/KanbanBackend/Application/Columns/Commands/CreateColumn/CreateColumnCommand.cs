using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.CreateColumn
{

    public record CreateColumnCommand(
        int BoardId,
        string Name
    ) : IRequest<ColumnDto>;

}
