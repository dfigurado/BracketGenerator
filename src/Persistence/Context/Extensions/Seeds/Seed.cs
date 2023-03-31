using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context.Extensions.Seeds
{
    public static class Seed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Tournament>().HasData(
                new Tournament
                {
                    Id = 1,
                    Name = "World Cup 2022"
                }
            );

            _ = modelBuilder.Entity<Team>().HasData(
                 new Team
                 {
                     Id = 1,
                     Seed = "1A",
                     TeamName = "Netherlands",
                     EloRating = 1500,
                     TournamentId = 1
                 },
                new Team
                {
                    Id = 2,
                    Seed = "2A",
                    TeamName = "Qatar",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 3,
                    Seed = "1B",
                    TeamName = "England",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 4,
                    Seed = "2B",
                    TeamName = "USA",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 5,
                    Seed = "1C",
                    TeamName = "Argentina",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 6,
                    Seed = "2C",
                    TeamName = "Mexico",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 7,
                    Seed = "1D",
                    TeamName = "France",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 8,
                    Seed = "2D",
                    TeamName = "Denmark",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 9,
                    Seed = "1E",
                    TeamName = "Germany",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 10,
                    Seed = "2E",
                    TeamName = "Japan",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 11,
                    Seed = "1F",
                    TeamName = "Belgium",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 12,
                    Seed = "2F",
                    TeamName = "Canada",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 13,
                    Seed = "2G",
                    TeamName = "Cameroon",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 14,
                    Seed = "1H",
                    TeamName = "Portugal",
                    EloRating = 1500,
                    TournamentId = 1
                },
                new Team
                {
                    Id = 15,
                    Seed = "2H",
                    TeamName = "Uruguay",
                    EloRating = 1500,
                    TournamentId = 1
                });
        }
    }
}