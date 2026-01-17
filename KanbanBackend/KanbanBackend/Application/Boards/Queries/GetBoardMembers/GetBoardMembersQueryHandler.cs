using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetBoardMembers
{
    public class GetBoardMembersQueryHandler
    : IRequestHandler<GetBoardMembersQuery, BoardMembersDto>
    {
        private readonly IBoardRepository _repo;
        private readonly IMapper _mapper;

        public GetBoardMembersQueryHandler(IBoardRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BoardMembersDto> Handle(GetBoardMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _repo.GetMembersAsync(request.BoardId);

            var memberDtos = _mapper.Map<List<BoardMemberDto>>(members);

            var userDtos = memberDtos.Select(s => s.User).ToList();

            return new BoardMembersDto
            {
                BoardId = request.BoardId,
                Users = userDtos
            };
        }
    }

}
