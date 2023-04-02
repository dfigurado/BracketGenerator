using Application.BracketGenerator;
using Application.Common.Base.CQRS;
using Application.Dto;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Common.UnitOfWork;

namespace Application.Features.TournamentSim
{
    public class BracketGeneratorCommandHandler : BaseCommandHandler, IRequestHandler<BracketGeneratorCommand, object>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BracketGeneratorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger) : base(logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(BracketGeneratorCommand request, CancellationToken cancellationToken)
        {
            // Get Participating Teams
            var teams = _unitOfWork.GetRepository<Domain.Entities.Team>().GetAll().ToList();

            var worldCup = new WorldCupTournament(GetTeamDto(teams));

            worldCup.SimulateTournament();

            var winner = worldCup.GetTournamentWinner();
            var pathToVictory = worldCup.PathToVictory(winner);

            return new
            {
                winner,
                pathToVictory
            };
        }

        private List<TeamDto> GetTeamDto(List<Domain.Entities.Team> team)
        {
            return _mapper.Map<List<TeamDto>>(team);
        }
    }
}
