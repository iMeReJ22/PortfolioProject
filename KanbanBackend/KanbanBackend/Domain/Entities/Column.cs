namespace KanbanBackend.Domain.Entities
{
    public partial class Column
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int OrderIndex { get; set; }

        public int BoardId { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

        public virtual Board Board { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
