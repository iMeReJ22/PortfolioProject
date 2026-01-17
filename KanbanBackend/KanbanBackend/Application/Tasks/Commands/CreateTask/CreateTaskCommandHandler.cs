using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(ITaskRepository tasks, IMapper mapper)
        {
            _tasks = tasks;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken ct)
        {
            var orderIndex = await _tasks.GetNextOrderIndexAsync(request.ColumnId);

            var task = new Domain.Entities.Task
            {
                ColumnId = request.ColumnId,
                Title = request.Title,
                Description = request.Description,
                TaskTypeId = request.TaskTypeId,
                CreatedByUserId = request.CreatedByUserId,
                CreatedAt = DateTime.UtcNow,
                OrderIndex = orderIndex
            };

            await _tasks.AddAsync(task);

            return _mapper.Map<TaskDto>(task);
        }
    }

}
