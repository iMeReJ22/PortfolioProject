using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.ReorderTasks
{
    public class ReorderTasksCommandHandler
    : IRequestHandler<ReorderTasksCommand, Unit>
    {
        private readonly ITaskRepository _tasks;
        private readonly IActivityLoggerService _logger;

        public ReorderTasksCommandHandler(ITaskRepository tasks, IActivityLoggerService logger)
        {

            _logger = logger;
            _tasks = tasks;
        }

        public async Task<Unit> Handle(ReorderTasksCommand request, CancellationToken ct)
        {
            var existing = await _tasks.GetForColumnAsync(request.ColumnId);

            foreach (var dto in request.Tasks)
            {
                var task = existing.FirstOrDefault(t => t.Id == dto.TaskId);
                if (task != null)
                    task.OrderIndex = dto.OrderIndex;
            }

            await _tasks.ReorderAsync(request.ColumnId, existing);

            foreach (var task in existing)
            {
                await _logger.AddLogTaskAsync("Task Reordered", "reorderd in", task.Id);
            }

            return Unit.Value;
        }
    }

}
