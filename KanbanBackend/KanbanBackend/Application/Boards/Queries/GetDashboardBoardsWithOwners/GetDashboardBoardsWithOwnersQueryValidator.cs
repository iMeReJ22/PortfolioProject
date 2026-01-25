using FluentValidation;

namespace KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners
{
    public class GetDashboardBoardsWithOwnersQueryValidator : AbstractValidator<GetDashboardBoardsWithOwnersQuery>
    {
        public GetDashboardBoardsWithOwnersQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
