using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;
using Persistence.Context.Extensions;
using Persistence.Interfaces.DbContext;

namespace Persistence;

public class BracketsDbContext : DbContext, IBracketsDbContext
{
    public BracketsDbContext(DbContextOptions<BracketsDbContext> options) : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        ConfigureEntities(modelBuilder);
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MatchConfigurations());
        modelBuilder.ApplyConfiguration(new TeamConfigurations());
        modelBuilder.ApplyConfiguration(new TournamentConfigurations());
    }
}