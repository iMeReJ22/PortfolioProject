using FluentValidation;

namespace KanbanBackend.Application.Comments.Queries.GetCommentsForTask
{
    public class GetCommentsForTaskQueryValidator : AbstractValidator<GetCommentsForTaskQuery>
    {
        public GetCommentsForTaskQueryValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
        }
    }
}
