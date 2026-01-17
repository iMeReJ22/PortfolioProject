using KanbanBackend.Application.Common.DTOs;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.UpdateTag
{
    public record UpdateTagCommand(
    int Id,
    string Name,
    string Color
) : IRequest<TagDto>;

}
