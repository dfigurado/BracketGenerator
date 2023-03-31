using Application.Common.Base.CQRS;
using Application.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Common.UnitOfWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Features.Team.Query
{
    public class TournamentTeamsQueryHandler : BaseQueryHandler, IRequestHandler<TournamentTeamsQuery, IQueryable<TeamDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentTeamsQueryHandler(ILogger logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IQueryable<TeamDto>> Handle(TournamentTeamsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.GetRepository<Domain.Entities.Team>().GetAll().Where(x => x.TournamentId == request.TournamentId).ProjectTo<TeamDto>(_mapper.ConfigurationProvider).AsQueryable());
        }
    }
}