using FluentValidation;

namespace KanbanBackend.Application.Boards.Commands.RemoveBoard
{
    public class RemoveBoardMemberCommandValidator : AbstractValidator<RemoveBoardMemberCommand>
    {
        public RemoveBoardMemberCommandValidator()
        {
            RuleFor(x => x.BoardId)
                .GreaterThan(0);
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
