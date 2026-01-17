using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetBoardsForUser
{
    public class GetBoardsForUserQueryHandler
    : IRequestHandler<GetBoardsForUserQuery, IReadOnlyList<BoardDto>>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;

        public GetBoardsForUserQueryHandler(IBoardRepository boards, IMapper mapper)
        {
            _boards = boards;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BoardDto>> Handle(GetBoardsForUserQuery request, CancellationToken ct)
        {
            var boards = await _boards.GetForUserAsync(request.UserId);
            return _mapper.Map<IReadOnlyList<BoardDto>>(boards);
        }
    }
}
