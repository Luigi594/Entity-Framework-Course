using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class MovieTheaterConfig : IEntityTypeConfiguration<MovieTheater>
    {
        public void Configure(EntityTypeBuilder<MovieTheater> builder)
        {
            builder.Property(prop => prop.Name)
               .HasMaxLength(150)
               .IsRequired();

            //builder.HasOne(x => x.MovieOffer)
            //    .WithOne()
            //    .HasForeignKey<MovieOffer>(x => x.MovieTheaterId);
        }
    }
}
