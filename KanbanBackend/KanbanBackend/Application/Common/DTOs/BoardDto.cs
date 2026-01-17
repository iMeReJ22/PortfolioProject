using KanbanBackend.Domain.Entities;
using System.Collections.ObjectModel;

namespace KanbanBackend.Application.Common.DTOs
{
    public class BoardDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? OwnerId { get; set; }
        public ICollection<ColumnDto>? Columns { get; set; } = [];
        public ICollection<TagDto> Tags { get; set; } = [];
        public ICollection<BoardMemberDto> Members { get; set; } = [];
    }
}
