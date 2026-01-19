using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Infrastructure.Services.HandleRecursiveDelete
{
    public interface IHandleRecursiveDeleteService
    {
        System.Threading.Tasks.Task HandleBoardAsync(Board board);
        System.Threading.Tasks.Task HandleColumnsAsync(IReadOnlyCollection<Column> columns);
        System.Threading.Tasks.Task HandleTasksAsync(IReadOnlyCollection<Domain.Entities.Task> tasks);
        System.Threading.Tasks.Task HandleCommentsAsync(IReadOnlyCollection<TaskComment> comments);
        System.Threading.Tasks.Task HandleLogsAsync(IReadOnlyCollection<ActivityLog> logs);
    }
}
