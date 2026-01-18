using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Infrastructure.Services.PassHasher;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IUserRepository users,
            IPasswordHasher hasher,
            IMapper mapper)
        {
            _users = users;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var id = await _users.GetMaxId();
            var user = new User
            {
                Id = ++id,
                Email = request.Email,
                DisplayName = request.DisplayName,
                PasswordHash = _hasher.Hash(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _users.AddAsync(user);

            return _mapper.Map<UserDto>(user);
        }
    }


}
