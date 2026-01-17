using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler
    : IRequestHandler<DeleteTagCommand, Unit>
    {
        private readonly ITagRepository _tags;

        public DeleteTagCommandHandler(ITagRepository tags)
        {
            _tags = tags;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken ct)
        {
            var tag = await _tags.GetByIdAsync(request.Id);
            if (tag == null)
                throw new NotFoundException("Board", request.Id);

            await _tags.DeleteAsync(tag);

            return Unit.Value;
        }
    }

}
