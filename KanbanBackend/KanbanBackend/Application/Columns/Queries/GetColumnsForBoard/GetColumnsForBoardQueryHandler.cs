using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Columns.Queries.GetColumnsForBoard
{

    public class GetColumnsForBoardQueryHandler
        : IRequestHandler<GetColumnsForBoardQuery, IReadOnlyList<ColumnDto>>
    {
        private readonly IColumnRepository _columns;
        private readonly IMapper _mapper;

        public GetColumnsForBoardQueryHandler(IColumnRepository columns, IMapper mapper)
        {
            _columns = columns;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ColumnDto>> Handle(GetColumnsForBoardQuery request, CancellationToken ct)
        {
            var columns = await _columns.GetForBoardAsync(request.BoardId);
            return _mapper.Map<IReadOnlyList<ColumnDto>>(columns);
        }
    }

}
