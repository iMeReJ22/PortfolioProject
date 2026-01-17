namespace KanbanBackend.Domain.Entities
{
    public partial class Board
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int OwnerId { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

        public virtual ICollection<BoardMember> BoardMembers { get; set; } = new List<BoardMember>();

        public virtual ICollection<Column> Columns { get; set; } = new List<Column>();

        public virtual User Owner { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
