using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler
    : IRequestHandler<CreateTagCommand, TagDto>
    {
        private readonly ITagRepository _tags;
        private readonly IMapper _mapper;
        private readonly IActivityLoggerService _logger;

        public CreateTagCommandHandler(ITagRepository tags, IMapper mapper, IActivityLoggerService logger)

        {
            _tags = tags;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken ct)
        {
            var id = await _tags.GetMaxId();
            var tag = new Tag
            {
                Id = ++id,
                BoardId = request.BoardId,
                Name = request.Name,
                ColorHex = request.Color,
                CreatedAt = DateTime.UtcNow
            };

            await _tags.AddAsync(tag);
            await _logger.AddLogTagAsync("Tag Created", "created", id);

            return _mapper.Map<TagDto>(tag);
        }
    }

}
