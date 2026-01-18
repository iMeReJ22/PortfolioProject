using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Infrastructure.Services.HandleRecursiveDelete
{
    public interface IHandleRecursiveDeleteService
    {
        public System.Threading.Tasks.Task HandleColumns(ICollection<Column> columns);

        public System.Threading.Tasks.Task HandleTasks(ICollection<Domain.Entities.Task> tasks);
        public System.Threading.Tasks.Task HandleComments(ICollection<TaskComment> comments);
        public System.Threading.Tasks.Task HandleTags(ICollection<Tag> tags);
        public System.Threading.Tasks.Task HandleLogs(ICollection<ActivityLog> logs);
    }
}
