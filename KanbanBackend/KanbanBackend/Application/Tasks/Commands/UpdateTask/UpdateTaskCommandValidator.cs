using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            // Description nvarchar(max) - no maximum enforced
            RuleFor(x => x.TaskTypeId).GreaterThan(0);
        }
    }
}
