namespace KanbanBackend.Application.Common.DTOs
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int OrderIndex { get; set; }
        public int BoardId { get; set; }
        public ICollection<TaskDto> Tasks { get; set; } = [];
    }
}
