using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand(
    int ColumnId,
    string Title,
    string Description,
    int TaskTypeId,
    int CreatedByUserId
) : IRequest<TaskDto>;

}
