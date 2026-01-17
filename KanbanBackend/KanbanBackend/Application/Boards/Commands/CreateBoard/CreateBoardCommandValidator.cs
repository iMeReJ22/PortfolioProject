using FluentValidation;

namespace KanbanBackend.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);
            RuleFor(x => x.OwnerId)
                .GreaterThan(0);
        }
    }
}
