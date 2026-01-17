using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public class GetActivityForBoardHandler : IRequestHandler<GetActivityForBoardCommand, Unit>
    {
        private readonly IActivityLogRepository _logs;
        public GetActivityForBoardHandler(IActivityLogRepository logs)
        {
            _logs = logs;
        }
        public async Task<Unit> Handle(GetActivityForBoardCommand request, CancellationToken cancellationToken)
        {
            await _logs.GetForBoardAsync(request.boardId);
            return Unit.Value;
        }
    }
}
