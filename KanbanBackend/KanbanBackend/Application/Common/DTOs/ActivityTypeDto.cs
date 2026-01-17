namespace KanbanBackend.Application.Common.DTOs
{
    public class ActivityTypeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string DisplayName { get; set; }
    }
}
