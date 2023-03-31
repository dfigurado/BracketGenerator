using FluentValidation;

namespace Application.Features.Team.Command
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            _ = RuleFor(x => x.Seed)
                .NotEmpty().WithMessage("Seed is required")
                .NotNull().WithMessage("Seed is required");

            _ = RuleFor(x => x.TournamentId)
                .NotEmpty().WithMessage("TournamentId is required");
        }
    }
}
