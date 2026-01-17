using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.DeleteColumn
{

    public class DeleteColumnCommandHandler
        : IRequestHandler<DeleteColumnCommand, Unit>
    {
        private readonly IColumnRepository _columns;

        public DeleteColumnCommandHandler(IColumnRepository columns)
        {
            _columns = columns;
        }

        public async Task<Unit> Handle(DeleteColumnCommand request, CancellationToken ct)
        {
            var column = await _columns.GetByIdAsync(request.Id);
            if (column == null)
                throw new NotFoundException("Board", request.Id);

            await _columns.DeleteAsync(column);

            return Unit.Value;
        }
    }

}
