namespace KanbanBackend.Application.Columns.Commands.UpdateColumn
{
    public class UpdateColumnCommandValidator : AbstractValidator<UpdateColumnCommand>
    {
        public UpdateColumnCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
