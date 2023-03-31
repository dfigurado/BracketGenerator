using Application.Interfaces.TournamentSim;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TournamentSim
{
    public class BracketGeneratorCommand : IBracketGeneratorCommand, IRequest<object>
    {
    }
}
