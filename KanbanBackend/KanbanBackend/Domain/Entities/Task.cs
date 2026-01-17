namespace KanbanBackend.Domain.Entities
{
    public partial class Task
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int OrderIndex { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ColumnId { get; set; }

        public int TaskTypeId { get; set; }

        public int? CreatedByUserId { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

        public virtual Column Column { get; set; } = null!;

        public virtual User? CreatedByUser { get; set; }

        public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

        public virtual TaskType TaskType { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
