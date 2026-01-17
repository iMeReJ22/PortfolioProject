using FluentValidation;

namespace KanbanBackend.Application.Boards.Queries.GetBoardById
{
    public class GetBoardByIdQueryValidator : AbstractValidator<GetBoardByIdQuery>
    {
        public GetBoardByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
