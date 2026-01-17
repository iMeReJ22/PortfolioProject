using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using MediatR;

namespace KanbanBackend.Application.Columns.Commands.CreateColumn
{

    public class CreateColumnCommandHandler
        : IRequestHandler<CreateColumnCommand, ColumnDto>
    {
        private readonly IColumnRepository _columns;
        private readonly IMapper _mapper;

        public CreateColumnCommandHandler(IColumnRepository columns, IMapper mapper)
        {
            _columns = columns;
            _mapper = mapper;
        }

        public async Task<ColumnDto> Handle(CreateColumnCommand request, CancellationToken ct)
        {
            var orderIndex = await _columns.GetNextOrderIndexAsync(request.BoardId);

            var column = new Column
            {
                BoardId = request.BoardId,
                Name = request.Name,
                OrderIndex = orderIndex
            };

            await _columns.AddAsync(column);

            return _mapper.Map<ColumnDto>(column);
        }
    }

}
