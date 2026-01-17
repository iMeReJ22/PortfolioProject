using FluentValidation;

namespace KanbanBackend.Application.Boards.Queries.GetBoardMembers
{
    namespace KanbanBackend.Application.Boards.Queries.GetBoardMembers
    {
        public class GetBoardMembersQueryValidator : AbstractValidator<GetBoardMembersQuery>
        {
            public GetBoardMembersQueryValidator()
            {
                RuleFor(x => x.BoardId).GreaterThan(0);
            }
        }
    }
}
