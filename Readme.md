
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

/src/BracketGenerator.Core/Application/BracketGenerator/WorldCupTournament
        
        using Application.Dto;
using Application.Interfaces.BracketGenerator;

namespace Application.BracketGenerator
{
    public class WorldCupTournament : IWorldCupTournament
    {
        private readonly IList<TeamDto> _roundOfSixteen;
        private readonly Dictionary<string, List<TeamDto>> _bracket;

        private string _winner;

        public WorldCupTournament(IList<TeamDto> roundOf16)
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
            var currentRound = _roundOfSixteen;
            var nextRound = new List<TeamDto>();

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
    
Unit Tests
----------
/tests/BracketGenerator.Core/Application.Test/BracketGenerator/TournamentBracketTests 
        
        using Application.Dto;
using Application.Interfaces.BracketGenerator;
using Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Application.BracketGenerator.Tests;

public class TournamentBracketTests
{

    [Fact]
    public void Tournament_Simulation_Test()
    {
        //Arrage
        var teams = GenerateTeamsFromJson();

        var tournament = new WorldCupTournament(teams);

        // Act
        tournament.SimulateTournament();

        var winner = tournament.GetTournamentWinner();

        var pathToVictory = tournament.PathToVictory(winner);

        // Assert
        winner.ShouldNotBeNull();
        winner.ShouldNotBeEmpty();

        pathToVictory.ShouldNotBeNull();
        pathToVictory.ShouldNotBeEmpty();
    }

    private List<TeamDto> GenerateTeamsFromJson()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedFile.json");
        var json = File.ReadAllText(filePath);

        var jsonObject = JObject.Parse(json);
        var teamsJsonArray = (JArray)jsonObject["R16"];

        var teams = new List<TeamDto>();

        foreach (var teamJson in teamsJsonArray)
        {
            string seed = teamJson["Seed"].ToString();
            string teamName = teamJson["TeamName"].ToString();
            int eloRating = int.Parse(teamJson["EloRating"].ToString());

            teams.Add(new TeamDto
            {
                Seed = seed,
                TeamName = teamName,
                EloRating = eloRating
            });
        }

        return teams;
    }
}
    
### Unit test for these has writen and they pass. 
    
### Issues 

There is a DI issue with when runing the API project. 
