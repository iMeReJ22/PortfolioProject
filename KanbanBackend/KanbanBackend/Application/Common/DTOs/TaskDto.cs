namespace KanbanBackend.Application.Common.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ColumnId { get; set; }
        public int TaskTypeId { get; set; }
        public int? CreatedByUserId { get; set; }
        public ICollection<TagDto> Tags { get; set; } = [];
        public ICollection<TaskCommentDto> Comments { get; set; } = [];
    }
}
