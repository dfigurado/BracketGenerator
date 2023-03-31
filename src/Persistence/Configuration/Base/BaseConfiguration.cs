using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Base
{
    public class BaseConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.Property(p => p.CreatedBy).IsRequired().HasDefaultValue("Migration");
            builder.Property(p => p.CreatedOn).IsRequired().HasConversion(r => r, r => DateTime.SpecifyKind(r, DateTimeKind.Utc));

            builder.Property(p => p.ModifiedBy).IsRequired().IsRequired().HasDefaultValue("Migration");
            builder.Property(p => p.ModifiedOn).IsRequired().HasConversion(r => r, r => DateTime.SpecifyKind(r, DateTimeKind.Utc));
        }
    }
}