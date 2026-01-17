using FluentValidation;

namespace KanbanBackend.Application.Boards.Commands.AddBoardMember
{
    public class AddBoardMemberCommandValidator : AbstractValidator<AddBoardMemberCommand>
    {
        public AddBoardMemberCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.BoardId)
                .GreaterThan(0);

            RuleFor(x => x.Role)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
