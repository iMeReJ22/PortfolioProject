using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler
    : IRequestHandler<CreateCommentCommand, TaskCommentDto>
    {
        private readonly ITaskCommentRepository _comments;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public CreateCommentCommandHandler(ITaskCommentRepository comments, IMapper mapper, IActivityLoggerService logger)
        {
            _comments = comments;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TaskCommentDto> Handle(CreateCommentCommand request, CancellationToken ct)
        {
            var id = await _comments.GetMaxId();
            var comment = new TaskComment
            {
                Id = ++id,
                TaskId = request.TaskId,
                AuthorId = request.AuthorId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _comments.AddAsync(comment);

            await _logger.AddLogCommentAsync("Comment Added", "added to", id);

            return _mapper.Map<TaskCommentDto>(comment);
        }
    }

}
