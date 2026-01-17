namespace KanbanBackend.Domain.Entities
{
    public partial class ActivityType
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    }
}
