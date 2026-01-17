using FluentValidation;

namespace KanbanBackend.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
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
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
