using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler
    : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly ITaskCommentRepository _comments;

        public DeleteCommentCommandHandler(ITaskCommentRepository comments)
        {
            _comments = comments;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken ct)
        {
            var comment = await _comments.GetByIdAsync(request.Id);
            if (comment == null)
                throw new NotFoundException("Board", request.Id);

            await _comments.DeleteAsync(comment);

            return Unit.Value;
        }
    }

}
