using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Unit>
    {
        private readonly IUserRepository _users;
        public GetUserByIdQueryHandler(IUserRepository users)
        {
            _users = users;
        }
        public async Task<Unit> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            await _users.GetByIdAsync(request.userId);

            return Unit.Value;
        }
    }
}
