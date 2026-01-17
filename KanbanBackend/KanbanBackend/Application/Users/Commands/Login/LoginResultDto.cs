using KanbanBackend.Application.Common.DTOs;

namespace KanbanBackend.Application.Users.Commands.Login
{
    public class LoginResultDto
    {
        public string Token { get; set; } = default!;
        public UserDto User { get; set; } = default!;
    }
}
