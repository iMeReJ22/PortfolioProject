using AutoMapper;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners
{
    public class GetDashboardBoardsWithOwnersQueryHandler : IRequestHandler<GetDashboardBoardsWithOwnersQuery, IReadOnlyList<BoardTileDto>>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;

        public GetDashboardBoardsWithOwnersQueryHandler(IBoardRepository boards, IMapper mapper)
        {
            _boards = boards;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BoardTileDto>> Handle(GetDashboardBoardsWithOwnersQuery request, CancellationToken cancellationToken)
        {
            var boards = await _boards.GetDashboardForUser(request.UserId);
            return _mapper.Map<IReadOnlyList<BoardTileDto>>(boards);
        }
    }
}
