using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Users.Queries.GetUsers
{
    public record GetUserQuery() : IRequest<Unit>
    { 
    }
}
