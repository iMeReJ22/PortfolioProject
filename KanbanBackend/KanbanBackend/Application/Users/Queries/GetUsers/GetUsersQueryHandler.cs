using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, Unit>
    {
        private readonly IUserRepository _users;

        public GetUsersQueryHandler(IUserRepository users)
        {
            _users = users;
        }
        public async Task<Unit> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            await _users.GetUsers();
            return Unit.Value;
        }
    }
}
