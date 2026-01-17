using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.UpdateColumn
{

    public record UpdateColumnCommand(
        int Id,
        string Name
    ) : IRequest<ColumnDto>;

}
