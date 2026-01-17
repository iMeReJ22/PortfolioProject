using FluentValidation;

namespace KanbanBackend.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty();
            // Password is plain text here; no DB max specified (PasswordHash has length in DB)
        }
    }
}
