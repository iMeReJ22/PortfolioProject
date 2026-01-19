using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.CreateColumn
{

    public class CreateColumnCommandHandler
        : IRequestHandler<CreateColumnCommand, ColumnDto>
    {
        private readonly IColumnRepository _columns;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public CreateColumnCommandHandler(IColumnRepository columns, IMapper mapper, IActivityLoggerService logger)
        {
            _columns = columns;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ColumnDto> Handle(CreateColumnCommand request, CancellationToken ct)
        {
            var orderIndex = await _columns.GetNextOrderIndexAsync(request.BoardId);
            var id = await _columns.GetMaxId();
            var column = new Column
            {
                Id = ++id,
                BoardId = request.BoardId,
                Name = request.Name,
                OrderIndex = orderIndex
            };

            await _columns.AddAsync(column);

            await _logger.AddLogColumnAsync("Columnt Created", "created", id);

            return _mapper.Map<ColumnDto>(column);
        }
    }

}
