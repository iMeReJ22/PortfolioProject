using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.MoveTask
{
    public class MoveTaskCommandHandler
    : IRequestHandler<MoveTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;

        public MoveTaskCommandHandler(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        public async Task<Unit> Handle(MoveTaskCommand request, CancellationToken ct)
        {
            var task = await _tasks.GetByIdAsync(request.TaskId);
            if (task == null)
                throw new NotFoundException("Board", request.TaskId);

            task.ColumnId = request.TargetColumnId;
            task.OrderIndex = request.NewOrderIndex;

            await _tasks.UpdateAsync(task);

            return Unit.Value;
        }
    }

}
