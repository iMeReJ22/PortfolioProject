using FluentValidation;

namespace KanbanBackend.Application.Users.Queries.GetUsersByBoard
{
    public class GetUsersByBoardQueryValidator : AbstractValidator<GetUsersByBoardQuery>
    {
        public GetUsersByBoardQueryValidator()
        {
            RuleFor(x => x.boardId)
                .GreaterThan(0);
        }
    }
}
