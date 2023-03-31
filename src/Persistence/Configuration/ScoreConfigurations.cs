using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configuration.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class ScoreConfigurations : BaseConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
        }
    }
}
