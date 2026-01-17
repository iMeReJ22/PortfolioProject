using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler
    : IRequestHandler<UpdateTagCommand, TagDto>
    {
        private readonly ITagRepository _tags;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(ITagRepository tags, IMapper mapper)
        {
            _tags = tags;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken ct)
        {
            var tag = await _tags.GetByIdAsync(request.Id);
            if (tag == null)
                throw new NotFoundException("Board", request.Id);

            tag.Name = request.Name;
            tag.ColorHex = request.Color;

            await _tags.UpdateAsync(tag);

            return _mapper.Map<TagDto>(tag);
        }
    }

}
