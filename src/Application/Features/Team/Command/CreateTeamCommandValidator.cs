using FluentValidation;

namespace Application.Features.Team.Command
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Teams)
                .NotEmpty().WithMessage("Team is required")
                .NotNull().WithMessage("Team is required");

            RuleFor(x => x.TournamentId)
                .NotEmpty().WithMessage("TournamentId is required");
        }
    }
}
