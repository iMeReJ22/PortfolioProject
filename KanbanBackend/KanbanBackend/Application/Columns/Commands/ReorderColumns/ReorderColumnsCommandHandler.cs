using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.ReorderColumns
{

    public class ReorderColumnsCommandHandler
        : IRequestHandler<ReorderColumnsCommand, Unit>
    {
        private readonly IColumnRepository _columns;

        public ReorderColumnsCommandHandler(IColumnRepository columns)
        {
            _columns = columns;
        }

        public async Task<Unit> Handle(ReorderColumnsCommand request, CancellationToken ct)
        {
            var existing = await _columns.GetForBoardAsync(request.BoardId);

            foreach (var dto in request.Columns)
            {
                var col = existing.FirstOrDefault(c => c.Id == dto.ColumnId);
                if (col != null)
                    col.OrderIndex = dto.OrderIndex;
            }

            await _columns.ReorderAsync(request.BoardId, existing);

            return Unit.Value;
        }
    }

}
