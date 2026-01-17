namespace KanbanBackend.Application.Common.DTOs
{
    public class TaskTypeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ColorHex { get; set; }
        public int OrderIndex { get; set; }
    }
}
