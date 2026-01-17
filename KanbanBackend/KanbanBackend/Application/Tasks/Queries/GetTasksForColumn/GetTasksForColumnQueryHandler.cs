using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTasksForColumn
{
    public class GetTasksForColumnQueryHandler
    : IRequestHandler<GetTasksForColumnQuery, IReadOnlyList<TaskDto>>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;

        public GetTasksForColumnQueryHandler(ITaskRepository tasks, IMapper mapper)
        {
            _tasks = tasks;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TaskDto>> Handle(GetTasksForColumnQuery request, CancellationToken ct)
        {
            var tasks = await _tasks.GetForColumnAsync(request.ColumnId);
            return _mapper.Map<IReadOnlyList<TaskDto>>(tasks);
        }
    }

}
