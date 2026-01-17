using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler
    : IRequestHandler<UpdateCommentCommand, TaskCommentDto>
    {
        private readonly ITaskCommentRepository _comments;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(ITaskCommentRepository comments, IMapper mapper)
        {
            _comments = comments;
            _mapper = mapper;
        }

        public async Task<TaskCommentDto> Handle(UpdateCommentCommand request, CancellationToken ct)
        {
            var comment = await _comments.GetByIdAsync(request.Id);
            if (comment == null)
                throw new NotFoundException("Board", request.Id);

            comment.Content = request.Content;
            comment.CreatedAt = DateTime.UtcNow;

            await _comments.UpdateAsync(comment);

            return _mapper.Map<TaskCommentDto>(comment);
        }
    }

}
