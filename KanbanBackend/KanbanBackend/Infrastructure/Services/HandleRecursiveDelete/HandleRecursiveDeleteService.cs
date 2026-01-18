using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using System.Runtime.CompilerServices;

namespace KanbanBackend.Infrastructure.Services.HandleRecursiveDelete
{
    public class HandleRecursiveDeleteService : IHandleRecursiveDeleteService
    {
        private readonly IColumnRepository _columns;
        private readonly ITagRepository _tags;
        private readonly ITaskCommentRepository _comments;
        private readonly ITaskRepository _tasks;
        private readonly IActivityLogRepository _logs;

        public HandleRecursiveDeleteService(IColumnRepository columns, 
            ITagRepository tags, 
            ITaskCommentRepository comments, 
            ITaskRepository tasks, 
            IActivityLogRepository logs)
        {
            _columns = columns;
            _tags = tags;   
            _comments = comments;
            _tasks = tasks;
            _logs = logs;
        }
        public System.Threading.Tasks.Task HandleColumns(ICollection<Column> columns)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task HandleComments(ICollection<TaskComment> comments)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task HandleLogs(ICollection<ActivityLog> logs)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task HandleTags(ICollection<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task HandleTasks(ICollection<Domain.Entities.Task> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
