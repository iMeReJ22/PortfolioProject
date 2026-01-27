using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTaskTypes
{
    public class GetTaskTypesQueryHandler : IRequestHandler<GetTaskTypesQuery, IReadOnlyList<TaskTypeDto>>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;
        public GetTaskTypesQueryHandler(ITaskRepository tasks, IMapper mapper)
        {
            _tasks = tasks;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<TaskTypeDto>> Handle(GetTaskTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _tasks.GetTaskTypesAsync();
            return _mapper.Map<IReadOnlyList<TaskTypeDto>>(types);
        }
    }
}
