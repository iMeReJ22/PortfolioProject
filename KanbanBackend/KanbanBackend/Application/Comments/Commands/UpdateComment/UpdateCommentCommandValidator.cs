using FluentValidation;

namespace KanbanBackend.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Content).NotEmpty(); // nvarchar(max)
        }
    }
}
