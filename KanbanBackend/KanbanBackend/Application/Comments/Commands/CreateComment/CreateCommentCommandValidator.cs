using FluentValidation;

namespace KanbanBackend.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
            RuleFor(x => x.AuthorId).GreaterThan(0);
            RuleFor(x => x.Content).NotEmpty(); // TaskComments.Content is nvarchar(max) in SQL
        }
    }
}
