using FluentValidation;

namespace KanbanBackend.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.userId)
                .GreaterThan(0);
        }
    }
}
