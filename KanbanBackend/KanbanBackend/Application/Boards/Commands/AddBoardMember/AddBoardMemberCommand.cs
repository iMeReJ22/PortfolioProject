using MediatR;

namespace KanbanBackend.Application.Boards.Commands.AddBoardMember
{
    public record AddBoardMemberCommand(
    int BoardId,
    int UserId,
    string Role
) : IRequest<Unit>;

}
