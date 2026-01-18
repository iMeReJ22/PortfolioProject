using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, IReadOnlyList<UserDto>>
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _users.GetUsers();
            return _mapper.Map<IReadOnlyList<UserDto>>(users);
        }
    }
}
