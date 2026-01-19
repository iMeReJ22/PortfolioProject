using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler
    : IRequestHandler<DeleteTagCommand, Unit>
    {
        private readonly ITagRepository _tags;
        private readonly IActivityLoggerService _logger;

        public DeleteTagCommandHandler(ITagRepository tags, IActivityLoggerService logger)
        {
            _tags = tags;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken ct)
        {
            var tag = await _tags.GetByIdAsync(request.Id);
            if (tag == null)
                throw new NotFoundException("Board", request.Id);

            await _tags.DeleteAsync(tag);

            await _logger.AddLogTagAsync("Tag Removed", "removed", tag.Id);

            return Unit.Value;
        }
    }

}
