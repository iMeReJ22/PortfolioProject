using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using KanbanBackend.Infrastructure.Services.HandleRecursiveDelete;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.DeleteColumn
{

    public class DeleteColumnCommandHandler
        : IRequestHandler<DeleteColumnCommand, Unit>
    {
        private readonly IColumnRepository _columns;
        private readonly IHandleRecursiveDeleteService _recursive;
        private readonly IActivityLoggerService _logger;

        public DeleteColumnCommandHandler(IColumnRepository columns, IHandleRecursiveDeleteService recursive, IActivityLoggerService logger)
        {
            _columns = columns;
            _recursive = recursive;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteColumnCommand request, CancellationToken ct)
        {
            var column = await _columns.GetByIdAsync(request.Id);
            if (column == null)
                throw new NotFoundException("Board", request.Id);

            await _recursive.HandleColumnsAsync([column]);

            await _logger.AddLogColumnAsync("Column Deleted", "deleted", request.Id);

            return Unit.Value;
        }
    }

}
