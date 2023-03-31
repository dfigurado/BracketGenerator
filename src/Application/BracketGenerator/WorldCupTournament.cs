using Application.Dto;
using Application.Interfaces.BracketGenerator;

namespace Application.BracketGenerator
{
    public class WorldCupTournament : IWorldCupTournament
    {
        private readonly List<TeamDto> _roundOfSixteen;
        private readonly Dictionary<string, List<TeamDto>> _bracket;

        private string _winner;

        public WorldCupTournament(List<TeamDto> roundOf16)
        {
            _roundOfSixteen = roundOf16;
            _bracket = new Dictionary<string, List<TeamDto>>();
            _winner = string.Empty;
        }

        public void SeedTeam(string seed, string team)
        {
            _roundOfSixteen.Add(new TeamDto { Seed = seed, TeamName = team });
        }

        public void AdvanceTeam(TeamDto winningTeam, TeamDto loosingTeam)
        {
            if (!_bracket.ContainsKey(winningTeam.TeamName))
            {
                _bracket[winningTeam.TeamName] = new List<TeamDto>();
            }

            _bracket[winningTeam.TeamName].Add(loosingTeam);
        }


        public string GetTournamentWinner()
        {
            return _winner;
        }

        public List<string> PathToVictory(string teamName)
        {
            if (_bracket.TryGetValue(teamName, out var path))
            {
                List<string> pathToVictory = new List<string>();

                foreach (var team in path)
                {
                    pathToVictory.Add(team.TeamName);
                }

                return pathToVictory;
            }

            return new List<string>();
        }

        public void SimulateTournament()
        {
            List<TeamDto> currentRound = _roundOfSixteen;
            List<TeamDto> nextRound = new List<TeamDto>();

            while (currentRound.Count > 1)
            {
                for (int i = 0; i < currentRound.Count; i += 2)
                {
                    //Simulate a match between the two teams, assuming the team with the lower seed wins.
                    TeamDto team1 = currentRound[i];
                    TeamDto team2 = currentRound[i + 1];

                    TeamDto winner, looser;

                    if (team1.EloRating > team2.EloRating)
                    {
                        winner = team1;
                        looser = team2;
                    }
                    else
                    {
                        winner = team2;
                        looser = team1;
                    }

                    UpdateEloRatings(winner, looser);
                    AdvanceTeam(winner, looser);
                    nextRound.Add(winner);
                }

                currentRound = nextRound;
                nextRound = new List<TeamDto>();
            }

            _winner = currentRound[0].TeamName;
        }

        private int CalculateEloWinProbability(int rating1, int rating2)
        {
            return 1 / (1 + (int)Math.Pow(10, (double)(rating2 - rating1) / 400));
        }

        private void UpdateEloRatings(TeamDto winner, TeamDto loser)
        {
            const int kFactor = 32;

            int winProbabilityLoser = CalculateEloWinProbability(loser.EloRating, winner.EloRating);
            int winProbabilityWinner = CalculateEloWinProbability(winner.EloRating, loser.EloRating);

            winner.EloRating += kFactor * (1 - winProbabilityWinner);
            loser.EloRating += kFactor * (0 - winProbabilityLoser);
        }

    }
}
