using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace KanbanBackend.Application.ActivityLog.Commands.CreateActivityLogTask
{
    public class CreateActivityLogTaskCommandHandler
    : IRequestHandler<CreateActivityLogTaskCommand, ActivityLogDto>
    {
        private readonly IActivityLogRepository _logs;
        private readonly IMapper _mapper;

        public CreateActivityLogTaskCommandHandler(IActivityLogRepository logs, IMapper mapper)
        {
            _logs = logs;
            _mapper = mapper;
        }

        public async Task<ActivityLogDto> Handle(CreateActivityLogTaskCommand request, CancellationToken ct)
        {
            var id = await _logs.GetMaxId();
            var log = new Domain.Entities.ActivityLog
            {
                Id = ++id,
                BoardId = request.BoardId,
                TaskId = request.TaskId,
                UserId = request.UserId,
                Name = request.Name ?? "No Name",
                Description = request.Description ?? "No Description",
                CreatedAt = DateTime.UtcNow
            };

            await _logs.AddAsync(log);


            return _mapper.Map<ActivityLogDto>(log);
        }
    }

}
