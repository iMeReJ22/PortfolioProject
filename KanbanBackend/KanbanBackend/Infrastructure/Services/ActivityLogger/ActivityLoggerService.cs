using Async = System.Threading.Tasks;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Domain.Exceptions;

namespace KanbanBackend.Infrastructure.Services.ActivityLogger
{
    public class ActivityLoggerService : IActivityLoggerService
    {

        private readonly IColumnRepository _columns;
        private readonly ITagRepository _tags;
        private readonly ITaskCommentRepository _comments;
        private readonly ITaskRepository _tasks;
        private readonly IActivityLogRepository _logs;
        private readonly IBoardRepository _boards;

        public ActivityLoggerService(IColumnRepository columns,
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
        private async Async.Task<ActivityLog> PrepareLog(string name, int boardId)
        {
            var logId = await _logs.GetMaxId();
            var log = new ActivityLog
            {
                Id = ++logId,
                Name = name,
                CreatedAt = DateTime.UtcNow,
                BoardId = boardId
            };
            return log;
        }
        public async Async.Task AddLogBoardAsync(string name, string description, int boardId)
        {
            var board = await _boards.GetBoardAsync(boardId);
            if (board == null)
                throw new NotFoundException("Board", boardId);

            var log = await PrepareLog(name, boardId);
            log.Description = $"Board '{board.Name}' has been {description}.";
            
            await _logs.AddAsync(log);
        }

        public async Async.Task AddLogBoardMemberAsync(string name, string description, int boardId, int userId)
        {
            var boardMember = await _boards.GetMemberAsync(boardId, userId);
            if (boardMember == null)
                throw new NotFoundException("BoardMember", boardId, userId);

            var log = await PrepareLog(name, boardId);
            log.Description = $"User '{boardMember.User.DisplayName} has been {description} Board '{boardMember.Board.Name}'.";
            log.UserId = userId;
            
            await _logs.AddAsync(log);
        }

        public async Async.Task AddLogColumnAsync(string name, string description, int columnId)
        {
            var column = await _columns.GetColumnAsync(columnId);
            if (column == null)
                throw new NotFoundException("Column", columnId);

            var log = await PrepareLog(name, column.BoardId);
            log.Description = $"Column '{column.Name}' has been {description}.";
            log.ColumnId = columnId;

            await _logs.AddAsync(log);
        }

        public async Async.Task AddLogCommentAsync(string name, string description, int commentId)
        {
            var comment = await _comments.GetCommentIdAsync(commentId);
            if (comment == null)
                throw new NotFoundException("Comment", commentId);

            var log = await PrepareLog(name, comment.Task.Column.BoardId);
            log.Description = $"Comment has been {description} Task '{comment.Task.Title}' in Column '{comment.Task.Column.Name}'.";
            log.TaskCommentId = commentId;

            await _logs.AddAsync(log);
        }

        public async Async.Task AddLogTagAsync(string name, string description, int tagId)
        {
            var tag = await _tags.GetTagAsync(tagId);
            if (tag == null)
                throw new NotFoundException("Tag", tagId);

            var log = await PrepareLog(name, tag.BoardId);
            log.Description = $"Tag '{tag.Name}' {description}.";
            log.TagId = tagId;

            await _logs.AddAsync(log);
        }

        public async Async.Task AddLogTaskAsync(string name, string description, int taskId)
        {
            var task = await _tasks.GetTaskAsync(taskId);
            if (task == null)
                throw new NotFoundException("Task", taskId);

            var log = await PrepareLog(name, task.Column.BoardId);
            log.Description = $"Task '{task.Title}' {description} '{task.Column.Name}'.";
            log.TaskId = taskId;

            await _logs.AddAsync(log);
        }

        public async Async.Task AddLogTagAssignAsync(string name, string description, int tagId, int taskId)
        {
            var tag = await _tags.GetByIdAsync(tagId);
            if (tag == null)
                throw new NotFoundException("Tag", tagId);
            var task = await _tasks.GetByIdAsync(taskId);
            if (task == null)
                throw new NotFoundException("Task", taskId);

            var log = await PrepareLog(name, tag.BoardId);
            log.Description = $"Tag '{tag.Name}' has been {description} '{task.Title}'.";
            log.TagId = tagId;
            log.TaskId = taskId;

            await _logs.AddAsync(log);
        }
    }
}
