using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler
    : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(ITaskRepository tasks, IMapper mapper)
        {
            _tasks = tasks;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken ct)
        {
            var task = await _tasks.GetByIdAsync(request.Id);
            if (task == null)
                throw new NotFoundException("Board", request.Id);

            return _mapper.Map<TaskDto>(task);
        }
    }

}
