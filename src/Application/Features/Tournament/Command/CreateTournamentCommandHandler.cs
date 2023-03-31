using Application.Common.Base.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Common.UnitOfWork;
using SoccoreTournament = Domain.Entities.Tournament;

namespace Application.Features.Tournament.Command
{
    public class CreateTournamentCommandHandler : BaseCommandHandler, IRequestHandler<CreateTournamentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTournamentCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            if (request.Tournament == null)
                return false;

            var tournament = await _unitOfWork.GetRepository<SoccoreTournament>().GetAll().FirstOrDefaultAsync(x => x.Name.Equals(request.Tournament.TournamentName));

            if (tournament != null)
                return false;

            var soccoreTournament = new SoccoreTournament
            {
                Name = request.Tournament.TournamentName,
                TournamentStartDate = request.Tournament.TournamentStartDate,
                TournamentEndDate = request.Tournament.TournamentEndDate
            };

            _unitOfWork.GetRepository<SoccoreTournament>().Add(soccoreTournament);

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
