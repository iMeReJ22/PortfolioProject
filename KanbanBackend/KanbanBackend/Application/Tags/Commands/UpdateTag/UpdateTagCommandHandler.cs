using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler
    : IRequestHandler<UpdateTagCommand, TagDto>
    {
        private readonly ITagRepository _tags;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public UpdateTagCommandHandler(ITagRepository tags, IMapper mapper, IActivityLoggerService logger)
        {
            _tags = tags;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken ct)
        {
            var tag = await _tags.GetByIdAsync(request.Id);
            if (tag == null)
                throw new NotFoundException("Board", request.Id);

            tag.Name = request.Name;
            tag.ColorHex = request.Color;

            await _tags.UpdateAsync(tag);

            await _logger.AddLogTagAsync("Tag Updated", "updated", tag.Id);

            return _mapper.Map<TagDto>(tag);
        }
    }

}
