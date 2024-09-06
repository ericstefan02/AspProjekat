using AspProjekat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.DataAccess.Configurations
{
    public class ProjectionTypeConfiguration : IEntityTypeConfiguration<ProjectionType>
    {
        public void Configure(EntityTypeBuilder<ProjectionType> builder)
        {
            builder.Property(x => x.Multiplier).IsRequired();
            builder.HasMany(x => x.Projections).WithOne(x => x.Type).HasForeignKey(x => x.TypeId);
        }
    }
}
