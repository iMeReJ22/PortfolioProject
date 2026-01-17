using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetBoardById
{
    public record GetBoardByIdQuery(int Id) : IRequest<BoardDto>;

}
