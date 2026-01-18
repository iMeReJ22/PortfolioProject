using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public class GetActivityForBoardQueryHandler : IRequestHandler<GetActivityForBoardQuery, Unit>
    {
        private readonly IActivityLogRepository _logs;
        public GetActivityForBoardQueryHandler(IActivityLogRepository logs)
        {
            _logs = logs;
        }
        public async Task<Unit> Handle(GetActivityForBoardQuery request, CancellationToken cancellationToken)
        {
            await _logs.GetForBoardAsync(request.boardId);
            return Unit.Value;
        }
    }
}
