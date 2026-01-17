using KanbanBackend.Application.Common.DTOs;

namespace KanbanBackend.Application.Boards.Queries.GetBoardMembers
{
    public class BoardMembersDto
    {
        public int BoardId { get; set; }
        public ICollection<UserDto> Users { get; set; } = [];
    }

}
