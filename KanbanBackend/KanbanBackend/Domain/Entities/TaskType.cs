namespace KanbanBackend.Domain.Entities
{
    public partial class TaskType
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ColorHex { get; set; } = null!;

        public int OrderIndex { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
