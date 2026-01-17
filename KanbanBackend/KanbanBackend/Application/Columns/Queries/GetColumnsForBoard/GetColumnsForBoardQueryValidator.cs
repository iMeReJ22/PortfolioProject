using FluentValidation;

namespace KanbanBackend.Application.Columns.Queries.GetColumnsForBoard
{
    public class GetColumnsForBoardQueryValidator : AbstractValidator<GetColumnsForBoardQuery>
    {
        public GetColumnsForBoardQueryValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
        }
    }
}
