using FluentValidation;

namespace KanbanBackend.Application.Tasks.Queries.GetTasksForBoard
{
    public class GetTasksForBoardQueryValidator : AbstractValidator<GetTasksForBoardQuery>
    {
        public GetTasksForBoardQueryValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0);
        }
    }
}
