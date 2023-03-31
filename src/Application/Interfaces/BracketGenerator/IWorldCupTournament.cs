using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.BracketGenerator
{
    public interface IWorldCupTournament
    {
        void SimulateTournament();
        string GetTournamentWinner();
        void SeedTeam(string seed, string team);
        void AdvanceTeam(TeamDto winningTeam, TeamDto loosingTeam);
        List<string> PathToVictory(string teamName);
    }
}
