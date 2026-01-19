using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _tasks;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public UpdateTaskCommandHandler(ITaskRepository tasks, IMapper mapper, IActivityLoggerService logger)
        {

            _logger = logger;
            _tasks = tasks;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken ct)
        {
            var task = await _tasks.GetByIdAsync(request.Id);
            if (task == null)
                throw new NotFoundException("Board", request.Id);

            task.Title = request.Title;
            task.Description = request.Description;
            task.TaskTypeId = request.TaskTypeId;

            await _tasks.UpdateAsync(task);

            await _logger.AddLogTaskAsync("Task Updated", "updated in", task.Id);

            return _mapper.Map<TaskDto>(task);
        }
    }

}
