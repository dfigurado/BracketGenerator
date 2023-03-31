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