using MediatR;

namespace KanbanBackend.Application.Boards.Commands.DeleteBoard
{
    public record DeleteBoardCommand(int Id) : IRequest<Unit>;
}
