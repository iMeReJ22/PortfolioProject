using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand, Unit>
    {
        private readonly ITaskRepository _tasks;

        public DeleteTaskCommandHandler(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken ct)
        {
            var task = await _tasks.GetByIdAsync(request.Id);
            if (task == null)
                throw new NotFoundException("Board", request.Id);

            await _tasks.DeleteAsync(task);

            return Unit.Value;
        }
    }

}
