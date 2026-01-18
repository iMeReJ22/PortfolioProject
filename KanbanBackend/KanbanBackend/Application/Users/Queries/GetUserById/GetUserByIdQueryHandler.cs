using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _users.GetByIdAsync(request.userId);
            if (user == null)
                throw new NotFoundException("User", request.userId);
            
            return _mapper.Map<UserDto>(user);
        }
    }
}
