namespace KanbanBackend.Domain.Entities
{
    public partial class ActivityLog
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int ActivityTypeId { get; set; }

        public int BoardId { get; set; }

        public int? UserId { get; set; }

        public int? TagId { get; set; }

        public int? ColumnId { get; set; }

        public int? TaskId { get; set; }

        public int? TaskCommentId { get; set; }

        public virtual ActivityType ActivityType { get; set; } = null!;

        public virtual Board Board { get; set; } = null!;

        public virtual Column? Column { get; set; }

        public virtual Tag? Tag { get; set; }

        public virtual Task? Task { get; set; }

        public virtual TaskComment? TaskComment { get; set; }

        public virtual User? User { get; set; }
    }
}
