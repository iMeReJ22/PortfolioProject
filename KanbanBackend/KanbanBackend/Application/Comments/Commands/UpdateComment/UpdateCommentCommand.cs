using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.UpdateComment
{
    public record UpdateCommentCommand(
    int Id,
    string Content
) : IRequest<TaskCommentDto>;

}
