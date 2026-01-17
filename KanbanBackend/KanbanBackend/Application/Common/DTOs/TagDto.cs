namespace KanbanBackend.Application.Common.DTOs
{
    public class TagDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ColorHex { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BoardId { get; set; }
    }
}
