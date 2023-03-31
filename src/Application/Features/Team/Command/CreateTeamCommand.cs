using Application.Dto;
using Application.Interfaces.Features.Team;
using MediatR;

using SoccerTeam = Domain.Entities.Team;

namespace Application.Features.Team.Command
{
    public class CreateTeamCommand : ICreateTeamCommand, IRequest<IList<SoccerTeam>>
    {
        public int TournamentId { get; set; }
        public IList<TeamDto> Seed { get; set; }
    }
}
