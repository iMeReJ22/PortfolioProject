using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetDetailedBoardById
{
    public class GetDetailedBoardByIdQueryHandler : IRequestHandler<GetDetailedBoardByIdQuery, BoardDto>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;

        public GetDetailedBoardByIdQueryHandler(IBoardRepository boards, IMapper mapper)
        {
            _boards = boards;
            _mapper = mapper;
        }
        public async Task<BoardDto> Handle(GetDetailedBoardByIdQuery request, CancellationToken cancellationToken)
        {
            var board = await _boards.GetBoardAsync(request.id);
            if (board == null)
                throw new NotFoundException("Board", request.id);

            return _mapper.Map<BoardDto>(board);
        }
    }
}
