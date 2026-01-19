using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler
    : IRequestHandler<UpdateCommentCommand, TaskCommentDto>
    {
        private readonly ITaskCommentRepository _comments;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public UpdateCommentCommandHandler(ITaskCommentRepository comments, IMapper mapper, IActivityLoggerService logger)
        {
            _comments = comments;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TaskCommentDto> Handle(UpdateCommentCommand request, CancellationToken ct)
        {
            var comment = await _comments.GetByIdAsync(request.Id);
            if (comment == null)
                throw new NotFoundException("Board", request.Id);

            comment.Content = request.Content;
            comment.CreatedAt = DateTime.UtcNow;

            await _comments.UpdateAsync(comment);
            await _logger.AddLogCommentAsync("Comment Updated", "updated on", comment.Id);
            return _mapper.Map<TaskCommentDto>(comment);
        }
    }

}
