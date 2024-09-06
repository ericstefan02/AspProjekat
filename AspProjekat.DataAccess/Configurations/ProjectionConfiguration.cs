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
    public class ProjectionConfiguration : IEntityTypeConfiguration<Projection>
    {
        public void Configure(EntityTypeBuilder<Projection> builder)
        {
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x=>x.Price).IsRequired();
            builder.HasOne(x => x.Movie).WithMany(x => x.Projections).HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.Type).WithMany(x => x.Projections).HasForeignKey(x => x.TypeId);
            builder.HasOne(x => x.Hall).WithMany(x => x.Projections).HasForeignKey(x => x.HallId);
        }
    }
}
