using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using MediatR;

namespace KanbanBackend.Application.Comments.Queries.GetCommentsForTask
{
    public class GetCommentsForTaskQueryHandler
    : IRequestHandler<GetCommentsForTaskQuery, IReadOnlyList<TaskCommentDto>>
    {
        private readonly ITaskCommentRepository _comments;
        private readonly IMapper _mapper;

        public GetCommentsForTaskQueryHandler(ITaskCommentRepository comments, IMapper mapper)
        {
            _comments = comments;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TaskCommentDto>> Handle(GetCommentsForTaskQuery request, CancellationToken ct)
        {
            var comments = await _comments.GetForTaskAsync(request.TaskId);
            return _mapper.Map<IReadOnlyList<TaskCommentDto>>(comments);
        }
    }

}
