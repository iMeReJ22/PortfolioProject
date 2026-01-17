using FluentValidation;

namespace KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard
{
    public class GetActivityForBoardValidator : AbstractValidator<GetActivityForBoardCommand>
    {
        public GetActivityForBoardValidator()
        {
            RuleFor(x => x.boardId)
                .GreaterThan(0);
        }
    }
}
