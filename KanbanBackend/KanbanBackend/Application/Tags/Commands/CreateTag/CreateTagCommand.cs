using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.CreateTag
{
    public record CreateTagCommand(
    int BoardId,
    string Name,
    string Color
) : IRequest<TagDto>;

}
