using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.UpdateColumn
{

    public class UpdateColumnCommandHandler
        : IRequestHandler<UpdateColumnCommand, ColumnDto>
    {
        private readonly IColumnRepository _columns;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public UpdateColumnCommandHandler(IColumnRepository columns, IMapper mapper, IActivityLoggerService logger)
        {
            _columns = columns;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ColumnDto> Handle(UpdateColumnCommand request, CancellationToken ct)
        {
            var column = await _columns.GetByIdAsync(request.Id);
            if (column == null)
                throw new NotFoundException("Board", request.Id);

            column.Name = request.Name;

            await _columns.UpdateAsync(column);

            await _logger.AddLogColumnAsync("Column Updated", "updated", request.Id);

            return _mapper.Map<ColumnDto>(column);
        }
    }

}
