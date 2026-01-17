using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetBoardById
{
    public class GetBoardByIdQueryHandler
    : IRequestHandler<GetBoardByIdQuery, BoardDto>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;

        public GetBoardByIdQueryHandler(IBoardRepository boards, IMapper mapper)
        {
            _boards = boards;
            _mapper = mapper;
        }

        public async Task<BoardDto> Handle(GetBoardByIdQuery request, CancellationToken ct)
        {
            var board = await _boards.GetByIdAsync(request.Id);
            if (board == null)
                throw new NotFoundException("Board", request.Id);

            return _mapper.Map<BoardDto>(board);
        }
    }

}
