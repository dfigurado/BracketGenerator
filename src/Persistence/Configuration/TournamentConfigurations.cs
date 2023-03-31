using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.Configuration;

public class TournamentConfigurations : BaseConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
    }
}