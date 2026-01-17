using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.AssignTagToTask
{
    public class AssignTagToTaskCommandValidator : AbstractValidator<AssignTagToTaskCommand>
    {
        public AssignTagToTaskCommandValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
            RuleFor(x => x.TagId).GreaterThan(0);
        }
    }
}
