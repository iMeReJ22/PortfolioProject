using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.ReorderColumns
{

    public class ReorderColumnsCommandHandler
        : IRequestHandler<ReorderColumnsCommand, Unit>
    {
        private readonly IColumnRepository _columns;
        private readonly IActivityLoggerService _logger;

        public ReorderColumnsCommandHandler(IColumnRepository columns, IActivityLoggerService logger)
        {
            _columns = columns;
            _logger = logger;
        }

        public async Task<Unit> Handle(ReorderColumnsCommand request, CancellationToken ct)
        {
            var existing = await _columns.GetForBoardAsync(request.BoardId);

            foreach (var dto in request.Columns)
            {
                var match = existing.FirstOrDefault(c => c.Id == dto.ColumnId);
                if (match != null)
                    match.OrderIndex = dto.OrderIndex;
            }

            await _columns.ReorderAsync(request.BoardId, existing);

            foreach (var column in existing)
            {
                await _logger.AddLogColumnAsync("Column Moved", "moved", column.Id);
            }

            return Unit.Value;
        }
    }

}
