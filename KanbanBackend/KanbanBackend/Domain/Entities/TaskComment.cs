namespace KanbanBackend.Domain.Entities
{
    public partial class TaskComment
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int TaskId { get; set; }

        public int? AuthorId { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

        public virtual User? Author { get; set; }

        public virtual Task Task { get; set; } = null!;
    }
}
