using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.RemoveTagFromTask
{
    public class RemoveTagFromTaskCommandHandler
    : IRequestHandler<RemoveTagFromTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;

        public RemoveTagFromTaskCommandHandler(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        public async Task<Unit> Handle(RemoveTagFromTaskCommand request, CancellationToken ct)
        {
            await _tasks.RemoveTagAsync(request.TaskId, request.TagId);
            return Unit.Value;
        }
    }

}
