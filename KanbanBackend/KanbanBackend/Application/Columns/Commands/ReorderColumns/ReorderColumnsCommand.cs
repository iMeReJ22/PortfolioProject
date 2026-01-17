using MediatR;

namespace KanbanBackend.Application.Columns.Commands.ReorderColumns
{

    public record ReorderColumnsCommand(
        int BoardId,
        IReadOnlyList<ColumnOrderDto> Columns
    ) : IRequest<Unit>;

    public record ColumnOrderDto(int ColumnId, int OrderIndex);

}
