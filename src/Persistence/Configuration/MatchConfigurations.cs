using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;
using System.Text.RegularExpressions;
using Match = Domain.Entities.Match;

namespace Persistence.Configuration;

public class MatchConfigurations : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(m => new { m.TeamAId, m.TeamBId, m.ScoreId, m.TournamentId });
    }
}