using FluentValidation;

namespace Application.Features.Tournament.Command
{
    public class CreateTournamentCommandValidator : AbstractValidator<CreateTournamentCommand>
    {
        public CreateTournamentCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            _ = RuleFor(x => x.Tournament)
                .NotEmpty().WithMessage("Tournament is required");
        }
    }
}