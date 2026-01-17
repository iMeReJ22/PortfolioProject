using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.CreateComment
{
    public record CreateCommentCommand(
    int TaskId,
    int AuthorId,
    string Content
) : IRequest<TaskCommentDto>;

}
