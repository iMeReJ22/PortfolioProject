using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Columns.Queries.GetColumnsForBoard
{

    public record GetColumnsForBoardQuery(int BoardId)
        : IRequest<IReadOnlyList<ColumnDto>>;

}
