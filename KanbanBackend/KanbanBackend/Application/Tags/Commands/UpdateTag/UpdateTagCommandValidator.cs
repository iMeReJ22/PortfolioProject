using FluentValidation;

namespace KanbanBackend.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Color)
                .NotEmpty()
                .MaximumLength(7)
                .Matches("^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{4}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$")
                .WithMessage("Color must be a valid hex color (e.g. #RGB, #RRGGBB, #RGBA, #RRGGBBAA).");
        }
    }
}
