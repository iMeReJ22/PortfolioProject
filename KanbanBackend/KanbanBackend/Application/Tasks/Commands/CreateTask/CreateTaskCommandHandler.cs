using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public CreateTaskCommandHandler(ITaskRepository tasks, IMapper mapper, IActivityLoggerService logger)

        {
            _tasks = tasks;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken ct)
        {
            var orderIndex = await _tasks.GetNextOrderIndexAsync(request.ColumnId);

            var id = await _tasks.GetMaxId();

            var task = new Domain.Entities.Task
            {
                Id = ++id,
                ColumnId = request.ColumnId,
                Title = request.Title,
                Description = request.Description,
                TaskTypeId = request.TaskTypeId,
                CreatedByUserId = request.CreatedByUserId,
                CreatedAt = DateTime.UtcNow,
                OrderIndex = orderIndex
            };

            await _tasks.AddAsync(task);
            await _logger.AddLogTaskAsync("Task Created", "created in", id);

            return _mapper.Map<TaskDto>(task);
        }
    }

}
