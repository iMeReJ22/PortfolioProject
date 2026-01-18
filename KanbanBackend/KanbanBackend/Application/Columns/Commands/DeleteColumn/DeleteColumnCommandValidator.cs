using FluentValidation;

namespace KanbanBackend.Application.Columns.Commands.DeleteColumn
{
    public class DeleteColumnCommandValidator : AbstractValidator<DeleteColumnCommand>
    {
        public DeleteColumnCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
