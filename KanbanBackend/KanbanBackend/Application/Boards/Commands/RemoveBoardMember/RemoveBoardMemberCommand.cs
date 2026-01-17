using MediatR;

namespace KanbanBackend.Application.Boards.Commands.RemoveBoard
{
    public record RemoveBoardMemberCommand(
    int BoardId,
    int UserId
) : IRequest<Unit>;

}
