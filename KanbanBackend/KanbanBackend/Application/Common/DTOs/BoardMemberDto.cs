namespace KanbanBackend.Application.Common.DTOs
{
    public class BoardMemberDto
    {
        public int UserId { get; set; }
        public int BoardId { get; set; }
        public required string Role { get; set; } = default!;
    }
}
