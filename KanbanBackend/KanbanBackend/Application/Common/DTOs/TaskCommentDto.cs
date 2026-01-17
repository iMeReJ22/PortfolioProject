namespace KanbanBackend.Application.Common.DTOs
{
    public class TaskCommentDto
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}
