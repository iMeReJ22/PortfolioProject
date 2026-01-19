using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public class GetActivityForBoardQueryHandler : IRequestHandler<GetActivityForBoardQuery, IReadOnlyList<ActivityLogDto>>
    {
        private readonly IActivityLogRepository _logs;
        private readonly IMapper _mapper;
        public GetActivityForBoardQueryHandler(IActivityLogRepository logs, IMapper mapper)
        {
            _mapper = mapper;
            _logs = logs;
        }
        public async Task<IReadOnlyList<ActivityLogDto>> Handle(GetActivityForBoardQuery request, CancellationToken cancellationToken)
        {
            var logs = await _logs.GetForBoardAsync(request.boardId);
            return _mapper.Map<IReadOnlyList<ActivityLogDto>>(logs);
        }
    }
}
