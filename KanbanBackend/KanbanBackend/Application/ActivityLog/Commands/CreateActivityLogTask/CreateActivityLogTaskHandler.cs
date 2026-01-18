using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.ActivityLog.Commands.CreateActivityLogTask
{
    public class CreateActivityLogTaskCommandHandler
    : IRequestHandler<CreateActivityLogTaskCommand, Unit>
    {
        private readonly IActivityLogRepository _logs;

        public CreateActivityLogTaskCommandHandler(IActivityLogRepository logs)
        {
            _logs = logs;
        }

        public async Task<Unit> Handle(CreateActivityLogTaskCommand request, CancellationToken ct)
        {
            var id = await _logs.GetMaxId();
            var log = new Domain.Entities.ActivityLog
            {
                Id = ++id,
                BoardId = request.BoardId,
                TaskId = request.TaskId,
                UserId = request.UserId,
                ActivityTypeId = request.ActionId,
                Description = request.Description != null ? request.Description : "No Description",
                CreatedAt = DateTime.UtcNow
            };

            await _logs.AddAsync(log);

            return Unit.Value;
        }
    }

}
