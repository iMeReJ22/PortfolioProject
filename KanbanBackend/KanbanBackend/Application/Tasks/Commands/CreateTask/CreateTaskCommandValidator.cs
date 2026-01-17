using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.ColumnId).GreaterThan(0);
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            // Description is nvarchar(max) — no maximum enforced
            RuleFor(x => x.TaskTypeId).GreaterThan(0);
            When(x => x.CreatedByUserId != 0, () => RuleFor(x => x.CreatedByUserId).GreaterThan(0));
        }
    }
}
