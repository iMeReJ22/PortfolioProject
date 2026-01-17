using FluentValidation;

namespace KanbanBackend.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
