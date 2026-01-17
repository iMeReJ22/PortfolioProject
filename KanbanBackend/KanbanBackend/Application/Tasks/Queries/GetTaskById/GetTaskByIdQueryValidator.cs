using FluentValidation;

namespace KanbanBackend.Application.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
    {
        public GetTaskByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
