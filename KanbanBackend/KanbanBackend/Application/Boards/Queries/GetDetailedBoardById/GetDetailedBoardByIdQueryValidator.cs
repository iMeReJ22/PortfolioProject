using FluentValidation;

namespace KanbanBackend.Application.Boards.Queries.GetDetailedBoardById
{
    public class GetDetailedBoardByIdQueryValidator : AbstractValidator<GetDetailedBoardByIdQuery>
    {
        public GetDetailedBoardByIdQueryValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
