using FluentValidation;

namespace KanbanBackend.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
