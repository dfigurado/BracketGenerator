using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interfaces.DbContext;

public interface IBracketsDbContext
{
    DbSet<Team> Teams { get; set; }
    DbSet<Match> Matches { get; set; }
    DbSet<Tournament> Tournaments { get; set; }
    DbSet<Score> Scores { get; set; }
}