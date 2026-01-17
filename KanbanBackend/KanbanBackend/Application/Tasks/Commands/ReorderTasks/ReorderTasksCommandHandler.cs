using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.ReorderTasks
{
    public class ReorderTasksCommandHandler
    : IRequestHandler<ReorderTasksCommand, Unit>
    {
        private readonly ITaskRepository _tasks;

        public ReorderTasksCommandHandler(ITaskRepository tasks)
        {
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

            return Unit.Value;
        }
    }

}
