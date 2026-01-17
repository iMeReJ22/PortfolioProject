using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.Authorization;
using KanbanBackend.Infrastructure.Services.PassHasher;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.Login
{
    public class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IAuthService _auth;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IUserRepository users,
            IPasswordHasher hasher,
            IAuthService auth,
            IMapper mapper)
        {
            _users = users;
            _hasher = hasher;
            _auth = auth;
            _mapper = mapper;
        }

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _users.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedException("Invalid email or password.");

            var valid = _hasher.Verify(user.PasswordHash, request.Password);
            if (!valid)
                throw new UnauthorizedException("Invalid email or password.");

            var token = _auth.GenerateJwtToken(user);

            return new LoginResultDto
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }
    }

}
