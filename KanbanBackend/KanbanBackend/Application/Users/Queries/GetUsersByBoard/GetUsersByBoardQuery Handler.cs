using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUsersByBoard
{
    public class GetUsersByBoardQueryHandler : IRequestHandler<GetUsersByBoardQuery, UserDto[]>
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public GetUsersByBoardQueryHandler(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }
        public async Task<UserDto[]> Handle(GetUsersByBoardQuery request, CancellationToken cancellationToken)
        {
            var users = await _users.GetUsersByBoardId(request.boardId);

            return _mapper.Map<UserDto[]>(users);
        }
    }
}
