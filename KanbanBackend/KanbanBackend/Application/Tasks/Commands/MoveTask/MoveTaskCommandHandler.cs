using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.MoveTask
{
    public class MoveTaskCommandHandler
    : IRequestHandler<MoveTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;
        private readonly IActivityLoggerService _logger;

        public MoveTaskCommandHandler(ITaskRepository tasks, IActivityLoggerService logger)

        {
            _tasks = tasks;
            _logger = logger;
        }

        public async Task<Unit> Handle(MoveTaskCommand request, CancellationToken ct)
        {
            var task = await _tasks.GetByIdAsync(request.TaskId);
            if (task == null)
                throw new NotFoundException("Board", request.TaskId);
            var descPart = task.ColumnId == request.TargetColumnId ? "in" : "from";
            await _tasks.MoveAsync(task, request.TargetColumnId, request.NewOrderIndex);
            await _logger.AddLogTaskAsync("Task Moved", $"moved {descPart}", task.Id);
            return Unit.Value;
        }
    }

}
