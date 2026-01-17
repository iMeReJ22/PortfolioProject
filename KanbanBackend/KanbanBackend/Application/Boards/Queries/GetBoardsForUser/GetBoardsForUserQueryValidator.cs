using FluentValidation;

namespace KanbanBackend.Application.Boards.Queries.GetBoardsForUser
{
    namespace KanbanBackend.Application.Boards.Queries.GetBoardsForUser
    {
        public class GetBoardsForUserQueryValidator : AbstractValidator<GetBoardsForUserQuery>
        {
            public GetBoardsForUserQueryValidator()
            {
                RuleFor(x => x.UserId).GreaterThan(0);
            }
        }
    }
}
