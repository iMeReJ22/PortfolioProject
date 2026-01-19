using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.RemoveTagFromTask
{
    public class RemoveTagFromTaskCommandHandler
    : IRequestHandler<RemoveTagFromTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;
        private readonly IActivityLoggerService _logger;

        public RemoveTagFromTaskCommandHandler(ITaskRepository tasks, IActivityLoggerService logger)

        {
            _tasks = tasks;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoveTagFromTaskCommand request, CancellationToken ct)
        {
            await _tasks.RemoveTagAsync(request.TaskId, request.TagId);
            await _logger.AddLogTagAssignAsync("Tag Removed From Task", "removed from", request.TagId, request.TaskId);

            return Unit.Value;
        }
    }

}
