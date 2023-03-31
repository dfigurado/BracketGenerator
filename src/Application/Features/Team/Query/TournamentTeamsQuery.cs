using Application.Dto;
using Application.Interfaces.Features.Team;
using MediatR;

namespace Application.Features.Team.Query
{
    public class TournamentTeamsQuery : ITournamentTeamsQuery, IRequest<IQueryable<TeamDto>>
    {
        public int TournamentId { get; set; }
    }
}