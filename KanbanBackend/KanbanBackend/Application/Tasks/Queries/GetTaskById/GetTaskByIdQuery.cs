using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTaskById
{
    public record GetTaskByIdQuery(int Id) : IRequest<TaskDto>;
}
