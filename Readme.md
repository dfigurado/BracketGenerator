
# Assumptions

Used the given JSON of 16 teams as the data source. 
Simulation purpose it was assumed that teams with low EloRating loose the game.

## Idea

Plan Was to implement a series of microservices that has endpoints to create tournament, add teams, simulate tournament, return a winner. 

## Core 

The given dataset contained seed and team. in order to simulate team advancement I used EloRating. an Initial EloRating is assigned to a team. while teams advace through the EloRating is updated. (Deduction of rating if lost was not done) only winning team rating was was updated. Wining Probability was calculated 

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

Logical considerations 
  - Initial Round has 16, 
  - 2nd round (Quater final) has 8 (4 matches)
  - 3rd round (Semi Final) has 4 (2 matches)
  - Final round has 2 teams. (1 match)
  - Winner.

IWorldCupTournament interface 

 - void SimulateTournament();
 - string GetTournamentWinner();
 - void SeedTeam(string seed, string team);
 - void AdvanceTeam(TeamDto winningTeam, TeamDto loosingTeam);
 - List<string> PathToVictory(string teamName);

WorldCupTournament concreat class implements IWorldCupTournament inteface.

/src/BracketGenerator.Core/Application/Interfaces/IWorldCupTournament
/src/BracketGenerator.Core/Application/BracketGenerator/WorldCupTournament
    
Unit Tests
----------
/tests/BracketGenerator.Core/Application.Test/BracketGenerator/TournamentBracketTests 
    
### Unit test for these has writen and they pass. 
    
### Issues 

There is a DI issue with when runing the API project. 
