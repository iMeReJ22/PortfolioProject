using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Comments.Queries.GetCommentsForTask
{
    public record GetCommentsForTaskQuery(int TaskId)
    : IRequest<IReadOnlyList<TaskCommentDto>>;

}
