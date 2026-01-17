using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Tags.Queries.GetTagsForBoard
{
    public class GetTagsForBoardQueryHandler
    : IRequestHandler<GetTagsForBoardQuery, IReadOnlyList<TagDto>>
    {
        private readonly ITagRepository _tags;
        private readonly IMapper _mapper;

        public GetTagsForBoardQueryHandler(ITagRepository tags, IMapper mapper)
        {
            _tags = tags;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TagDto>> Handle(GetTagsForBoardQuery request, CancellationToken ct)
        {
            var tags = await _tags.GetForBoardAsync(request.BoardId);
            return _mapper.Map<IReadOnlyList<TagDto>>(tags);
        }
    }

}
