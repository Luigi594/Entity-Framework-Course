using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(prop => prop.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(prop => prop.ReleaseDate)
                .HasColumnType("date");

            builder.Property(prop => prop.PosterUrl)
                .HasMaxLength(500)
                .IsUnicode(false); // Non-Unicode string (VARCHAR)

            builder.Property(x => x.TotalCopies);
            builder.Property(x => x.AvailableCopies);

            builder.Property(x => x.IsAvailableForRental)
                .HasDefaultValue(false);

        }
    }
}
