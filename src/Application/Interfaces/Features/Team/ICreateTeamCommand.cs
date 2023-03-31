using Application.Dto;

namespace Application.Interfaces.Features.Team
{
    public interface ICreateTeamCommand
    {
        public int TournamentId { get; set; }
        public IList<TeamDto> Seed { get; set; }
    }
}