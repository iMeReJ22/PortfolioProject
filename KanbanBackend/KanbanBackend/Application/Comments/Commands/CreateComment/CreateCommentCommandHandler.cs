using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using MediatR;

namespace KanbanBackend.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler
    : IRequestHandler<CreateCommentCommand, TaskCommentDto>
    {
        private readonly ITaskCommentRepository _comments;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(ITaskCommentRepository comments, IMapper mapper)
        {
            _comments = comments;
            _mapper = mapper;
        }

        public async Task<TaskCommentDto> Handle(CreateCommentCommand request, CancellationToken ct)
        {
            var comment = new TaskComment
            {
                TaskId = request.TaskId,
                AuthorId = request.AuthorId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _comments.AddAsync(comment);

            return _mapper.Map<TaskCommentDto>(comment);
        }
    }

}
