namespace KanbanBackend.Application.Common.DTOs
{
    public class ActivityLogDto
    {
        public int Id { get; set; }
        public required string Desription { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ActivityTypeId { get; set; }
        public int BoardId { get; set; }
        public int? UserId { get; set; }
        public int? TagId { get; set; }
        public int? ColumnId { get; set; }
        public int? TaskId { get; set; }
        public int? TaskCommentId { get; set; }
        public ActivityTypeDto? activityType { get; set; }
        public string? UserName { get; set; }
    }
}
