using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _users;
        public DeleteUserCommandHandler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.GetByIdAsync(request.id);
            if (user == null)
                throw new NotFoundException("User", request.id);

            await _users.DeleteAsync(user);
            return Unit.Value;
        }
    }
}
