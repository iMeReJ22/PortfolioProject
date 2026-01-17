using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand(
    int Id,
    string Title,
    string Description,
    int TaskTypeId
) : IRequest<TaskDto>;

}
