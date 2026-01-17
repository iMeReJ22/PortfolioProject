using FluentValidation;

namespace KanbanBackend.Application.Boards.Commands.UpdateBoard
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
