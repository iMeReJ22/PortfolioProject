using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.MoveTask
{
    public class MoveTaskCommandValidator : AbstractValidator<MoveTaskCommand>
    {
        public MoveTaskCommandValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
            RuleFor(x => x.TargetColumnId).GreaterThan(0);
            RuleFor(x => x.NewOrderIndex).GreaterThanOrEqualTo(0);
        }
    }
}
