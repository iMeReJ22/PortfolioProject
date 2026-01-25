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
        private readonly IBoardRepository _boards;

        public HandleRecursiveDeleteService(IColumnRepository columns, 
            ITagRepository tags, 
            ITaskCommentRepository comments, 
            ITaskRepository tasks, 
            IActivityLogRepository logs,
            IBoardRepository boards
            )
        {
            _columns = columns;
            _tags = tags;   
            _comments = comments;
            _tasks = tasks;
            _logs = logs;
            _boards = boards;
        }
        public async System.Threading.Tasks.Task HandleBoardAsync(Board board)
        {
            var logs = await _logs.GetForBoardAsync(board.Id);
            await HandleLogsAsync(logs);

            await _tags.RemoveAllTagsForBoardAsync(board.Id); 

            var columns = await _columns.GetForBoardAsync(board.Id);
            await HandleColumnsAsync(columns);

            var members = await _boards.GetMembersAsync(board.Id);
            await _boards.RemoveMembersRangeAsync(members);
            await _boards.DeleteAsync(board);
        }
        public async System.Threading.Tasks.Task HandleColumnsAsync(IReadOnlyCollection<Column> columns)
        {
            foreach (var column in columns)
            {
                var tasks = await _tasks.GetForColumnAsync(column.Id);
                await HandleTasksAsync(tasks);
            }

            await _columns.DeleteRangeAsync(columns);
        }
        public async System.Threading.Tasks.Task HandleTasksAsync(IReadOnlyCollection<Domain.Entities.Task> tasks)
        {
            foreach (var task in tasks)
            {
                var comments = await _comments.GetForTaskAsync(task.Id);

                var tags = await _tags.GetForTaskAsync(task.Id);
                if(tags.Count() > 0)
                    await _tags.RemoveAllTagsFromTaskAsync(task.Id);

                await HandleCommentsAsync(comments);
            }

            await _tasks.DeleteRangeAsync(tasks);
        }

        public async System.Threading.Tasks.Task HandleCommentsAsync(IReadOnlyCollection<TaskComment> comments)
        {
            await _comments.DeleteRangeAsync(comments);
        }

        public async System.Threading.Tasks.Task HandleLogsAsync(IReadOnlyCollection<ActivityLog> logs)
        {
            await _logs.DeleteRangeAsync(logs);
        }
    }
}
