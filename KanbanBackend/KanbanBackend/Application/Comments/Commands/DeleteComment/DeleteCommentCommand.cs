using MediatR;

namespace KanbanBackend.Application.Comments.Commands.DeleteComment
{
    public record DeleteCommentCommand(int Id) : IRequest<Unit>;

}
