using FluentValidation;

namespace KanbanBackend.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommandValidator : AbstractValidator<DeleteBoardCommand>
    {
        public DeleteBoardCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
