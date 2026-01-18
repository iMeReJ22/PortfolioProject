using FluentValidation;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public class GetActivityForBoardQueryValidator : AbstractValidator<GetActivityForBoardQuery>
    {
        public GetActivityForBoardQueryValidator()
        {
            RuleFor(x => x.boardId)
                .GreaterThan(0);
        }
    }
}
