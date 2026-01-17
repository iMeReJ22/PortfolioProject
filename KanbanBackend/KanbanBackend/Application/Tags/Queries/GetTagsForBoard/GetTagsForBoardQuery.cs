using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tags.Queries.GetTagsForBoard
{
    public record GetTagsForBoardQuery(int BoardId)
    : IRequest<IReadOnlyList<TagDto>>;

}
