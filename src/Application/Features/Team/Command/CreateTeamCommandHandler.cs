using Application.Common.Base.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Common.UnitOfWork;

using SoccerTeam = Domain.Entities.Team;

namespace Application.Features.Team.Command
{
    public class CreateTeamCommandHandler : BaseCommandHandler, IRequestHandler<CreateTeamCommand, IList<SoccerTeam>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<SoccerTeam>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            IList<SoccerTeam> teams = new List<SoccerTeam>();

            foreach (var t in request.Teams)
            {
                teams.Add(new SoccerTeam
                {
                    Seed = t.Seed,
                    TeamName = t.TeamName,
                    EloRating = Convert.ToInt32(t.EloRating),
                    TournamentId = request.TournamentId
                });
            }

            _unitOfWork.GetRepository<SoccerTeam>().AddRange(teams);

            await _unitOfWork.SaveChangesAsync();

            return _unitOfWork.GetRepository<SoccerTeam>().GetAll().Where(x => x.TournamentId == request.TournamentId).ToList<SoccerTeam>();
        }
    }
}
