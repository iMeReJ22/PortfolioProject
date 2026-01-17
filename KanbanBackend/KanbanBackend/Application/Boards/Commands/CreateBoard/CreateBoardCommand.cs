using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.CreateBoard
{
    public record CreateBoardCommand(
        string Name, 
        string Description, 
        int OwnerId)
    : IRequest<BoardDto>;

}
