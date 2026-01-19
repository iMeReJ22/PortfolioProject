using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using KanbanBackend.Infrastructure.Services.HandleRecursiveDelete;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;
        private readonly IHandleRecursiveDeleteService _recursive;
        private readonly IActivityLoggerService _logger;

        public DeleteTaskCommandHandler(ITaskRepository tasks, IHandleRecursiveDeleteService recursive, IActivityLoggerService logger)
        {
            _tasks = tasks;
            _recursive = recursive;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken ct)
        {
            var task = await _tasks.GetByIdAsync(request.Id);
            if (task == null)
                throw new NotFoundException("Board", request.Id);

            await _recursive.HandleTasksAsync([task]);

            await _logger.AddLogTaskAsync("Task Removed", "removed from", task.Id);

            return Unit.Value;
        }
    }

}
