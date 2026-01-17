using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTasksForBoard
{
    public class GetTasksForBoardQueryHandler
    : IRequestHandler<GetTasksForBoardQuery, IReadOnlyList<TaskDto>>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;

        public GetTasksForBoardQueryHandler(ITaskRepository tasks, IMapper mapper)
        {
            _tasks = tasks;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TaskDto>> Handle(GetTasksForBoardQuery request, CancellationToken ct)
        {
            var tasks = await _tasks.GetForBoardAsync(request.BoardId);
            return _mapper.Map<IReadOnlyList<TaskDto>>(tasks);
        }
    }

}
