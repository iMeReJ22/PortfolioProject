using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KanbanBackend.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler
    : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly ITaskCommentRepository _comments;
        private readonly IActivityLoggerService _logger;

        public DeleteCommentCommandHandler(ITaskCommentRepository comments, IActivityLoggerService logger)
        {
            _comments = comments;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken ct)
        {
            var comment = await _comments.GetByIdAsync(request.Id);
            if (comment == null)
                throw new NotFoundException("Board", request.Id);

            await _comments.DeleteAsync(comment);

            await _logger.AddLogCommentAsync("Comment Removed", "removed from", comment.Id);

            return Unit.Value;
        }
    }

}