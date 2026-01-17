using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.RemoveTagFromTask
{
    public class RemoveTagFromTaskCommandValidator : AbstractValidator<RemoveTagFromTaskCommand>
    {
        public RemoveTagFromTaskCommandValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
            RuleFor(x => x.TagId).GreaterThan(0);
        }
    }
}
