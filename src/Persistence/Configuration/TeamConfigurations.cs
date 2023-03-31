using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;

namespace Persistence.Configuration;

public class TeamConfigurations : BaseConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
    }
}