using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Features.Tournament
{
    public interface ICreateTournamentCommand
    {
        public TournamentDto Tournament { get; set; }
    }
}
