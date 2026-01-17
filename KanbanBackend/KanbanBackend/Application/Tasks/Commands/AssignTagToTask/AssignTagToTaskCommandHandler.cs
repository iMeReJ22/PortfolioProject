using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.AssignTagToTask
{
    public class AssignTagToTaskCommandHandler
    : IRequestHandler<AssignTagToTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;

        public AssignTagToTaskCommandHandler(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        public async Task<Unit> Handle(AssignTagToTaskCommand request, CancellationToken ct)
        {
            await _tasks.AssignTagAsync(request.TaskId, request.TagId);
            return Unit.Value;
        }
    }

}
