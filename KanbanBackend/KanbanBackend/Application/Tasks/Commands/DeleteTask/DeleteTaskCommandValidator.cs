using FluentValidation;

namespace KanbanBackend.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
