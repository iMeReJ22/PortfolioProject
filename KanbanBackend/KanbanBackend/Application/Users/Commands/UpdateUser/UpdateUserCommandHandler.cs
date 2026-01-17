using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.PassHasher;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler
     : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(
            IUserRepository users,
            IPasswordHasher hasher,
            IMapper mapper)
        {
            _users = users;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.Id);
            if (user == null)
                throw new NotFoundException("User", request.Id);

            // Aktualizacja istniejącego encja użytkownika
            user.Email = request.Email;
            user.DisplayName = request.DisplayName;
            user.PasswordHash = _hasher.Hash(request.Password);
            // CreatedAt pozostaje bez zmian

            await _users.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
