using Application.Dto;

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
