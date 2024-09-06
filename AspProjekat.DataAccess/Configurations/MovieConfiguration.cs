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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(512);
            builder.Property(x=>x.PremierDate).IsRequired();
            builder.Property(x=>x.DurationInMinutes).IsRequired();
            builder.Property(x=>x.BasePrice).IsRequired();
            builder.HasMany(x => x.Projections).WithOne(x => x.Movie).HasForeignKey(x => x.MovieId);
        }
    }
}
