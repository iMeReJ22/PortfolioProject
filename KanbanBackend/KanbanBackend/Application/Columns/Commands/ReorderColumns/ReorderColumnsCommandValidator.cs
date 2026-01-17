using FluentValidation;

namespace KanbanBackend.Application.Columns.Commands.ReorderColumns
{
    public class ReorderColumnsCommandValidator : AbstractValidator<ReorderColumnsCommand>
    {
        public ReorderColumnsCommandValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
            RuleFor(x => x.Columns).NotNull();
            RuleForEach(x => x.Columns).ChildRules(column =>
            {
                column.RuleFor(c => c.ColumnId).GreaterThan(0);
                column.RuleFor(c => c.OrderIndex).GreaterThanOrEqualTo(0);
            });
        }
    }
}
