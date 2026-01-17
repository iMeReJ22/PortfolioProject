using FluentValidation;

namespace KanbanBackend.Application.Columns.Commands.CreateColumn
{
    public class CreateColumnCommandValidator : AbstractValidator<CreateColumnCommand>
    {
        public CreateColumnCommandValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
