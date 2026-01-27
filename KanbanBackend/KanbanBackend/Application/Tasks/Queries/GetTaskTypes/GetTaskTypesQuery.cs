using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTaskTypes
{
    public record GetTaskTypesQuery: IRequest<IReadOnlyList<TaskTypeDto>>
    {
    }
}
