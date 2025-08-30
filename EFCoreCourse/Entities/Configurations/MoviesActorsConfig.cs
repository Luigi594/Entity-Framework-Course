using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class MoviesActorsConfig : IEntityTypeConfiguration<MoviesActors>
    {
        public void Configure(EntityTypeBuilder<MoviesActors> builder)
        {
            builder.HasKey(prop => new { prop.MovieId, prop.ActorId });

            builder.Property(prop => prop.Character)
                .HasMaxLength(150);
        }
    }
}
