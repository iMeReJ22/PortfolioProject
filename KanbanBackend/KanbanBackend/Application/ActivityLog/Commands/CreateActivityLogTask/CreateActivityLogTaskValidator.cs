using FluentValidation;

namespace KanbanBackend.Application.ActivityLog.Commands.CreateActivityLogTask
{
    public class CreateActivityLogTaskCommandValidator : AbstractValidator<CreateActivityLogTaskCommand>
    {
        public CreateActivityLogTaskCommandValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
            When(x => x.TaskId.HasValue, () => RuleFor(x => x.TaskId).GreaterThan(0));
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.ActionId).GreaterThan(0);

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description).MaximumLength(500);
            });
        }
    }
}