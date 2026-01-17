using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTasksForBoard
{
    public record GetTasksForBoardQuery(int BoardId)
    : IRequest<IReadOnlyList<TaskDto>>;

}
