using Application.Common.Base.CQRS;
using Application.Dto;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Common.UnitOfWork;
using Persistence.Interfaces.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (request.Seed.Count > 0)
                return null;

            IList<SoccerTeam> teams = new List<SoccerTeam>();

            foreach (var t in request.Seed)
            {
                teams.Add(new SoccerTeam
                {
                    Seed = t.Seed,
                    TeamName = t.TeamName,
                    TournamentId = request.TournamentId
                });
            }

            _unitOfWork.GetRepository<SoccerTeam>().AddRange(teams);

            await _unitOfWork.SaveChangesAsync();

            return _unitOfWork.GetRepository<SoccerTeam>().GetAll().Where(x => x.TournamentId == request.TournamentId).ToList<SoccerTeam>();
        }
    }
}
