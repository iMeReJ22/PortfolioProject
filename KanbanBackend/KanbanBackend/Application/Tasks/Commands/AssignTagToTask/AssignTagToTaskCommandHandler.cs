using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.AssignTagToTask
{
    public class AssignTagToTaskCommandHandler
    : IRequestHandler<AssignTagToTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;
        private readonly IActivityLoggerService _logger;

        public AssignTagToTaskCommandHandler(ITaskRepository tasks, IActivityLoggerService logger)
        {
            _tasks = tasks;
            _logger = logger;
        }

        public async Task<Unit> Handle(AssignTagToTaskCommand request, CancellationToken ct)
        {
            await _tasks.AssignTagAsync(request.TaskId, request.TagId);

            await _logger.AddLogTagAssignAsync("Tag Added To Task", "added to", request.TagId, request.TaskId);

            return Unit.Value;
        }
    }

}
