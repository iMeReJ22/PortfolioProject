namespace KanbanBackend.Domain.Entities
{
    public partial class BoardMember
    {
        public string Role { get; set; } = null!;

        public int BoardId { get; set; }

        public int UserId { get; set; }

        public virtual Board Board { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
