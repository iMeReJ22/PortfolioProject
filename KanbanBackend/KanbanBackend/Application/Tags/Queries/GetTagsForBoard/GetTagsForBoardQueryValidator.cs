using FluentValidation;

namespace KanbanBackend.Application.Tags.Queries.GetTagsForBoard
{
    public class GetTagsForBoardQueryValidator : AbstractValidator<GetTagsForBoardQuery>
    {
        public GetTagsForBoardQueryValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
        }
    }
}
