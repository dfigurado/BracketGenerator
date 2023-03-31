using Application.Dto;
using Application.Interfaces.Features.Tournament;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournament.Command
{
    public class CreateTournamentCommand : ICreateTournamentCommand, IRequest<bool>
    {
        public TournamentDto Tournament { get; set; }
    }
}
