using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTasksForColumn
{
    public record GetTasksForColumnQuery(int ColumnId)
     : IRequest<IReadOnlyList<TaskDto>>;

}
