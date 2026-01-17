using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.UpdateBoard
{
    public record UpdateBoardCommand(
        int Id,
        string Name,
        string Description
    ) : IRequest<BoardDto>;

}
