using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.ReorderTasks
{
    public class ReorderTasksCommandValidator : AbstractValidator<ReorderTasksCommand>
    {
        public ReorderTasksCommandValidator()
        {
            RuleFor(x => x.ColumnId).GreaterThan(0);
            RuleFor(x => x.Tasks).NotNull();
            RuleForEach(x => x.Tasks).ChildRules(task =>
            {
                task.RuleFor(t => t.TaskId).GreaterThan(0);
                task.RuleFor(t => t.OrderIndex).GreaterThanOrEqualTo(0);
            });
        }
    }
}
