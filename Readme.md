
# Assumptions

Used the given JSON of 16 teams as the data source. 
Simulation purpose it was assumed that teams with low EloRating loose the game.

## Idea

Plan Was to implement a series of microservices that has endpoints to create tournament, add teams, simulate tournament, return a winner. 

## Core 

The given dataset contained seed and team. in order to simulate team advancement I used EloRating. an Initial EloRating is assigned to a team.

Logical considerations 
  - Initial Round has 16, 
  - 2nd round (Quater final) has 8
  - 3rd round (Semi Final) has 4  
  - Final round has 2 teams. 
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
