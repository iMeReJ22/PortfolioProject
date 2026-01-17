using FluentValidation;

namespace KanbanBackend.Application.Tasks.Queries.GetTasksForColumn
{
    public class GetTasksForColumnQueryValidator : AbstractValidator<GetTasksForColumnQuery>
    {
        public GetTasksForColumnQueryValidator()
        {
            RuleFor(x => x.ColumnId).GreaterThan(0);
        }
    }
}
