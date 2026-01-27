using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Boards.Queries.GetDetailedBoardById
{
    public record GetDetailedBoardByIdQuery(int id) : IRequest<BoardDto>;
}
