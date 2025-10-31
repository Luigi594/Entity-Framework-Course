using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class MoviesActorsConfig : IEntityTypeConfiguration<MoviesActors>
    {
        public void Configure(EntityTypeBuilder<MoviesActors> builder)
        {
            builder.HasKey(prop => new { prop.MovieId, prop.ActorId });

            #region Example of configuring a relationship many to many on API Fluent

            //builder.HasOne(x => x.Actor)
            //    .WithMany(x => x.MoviesActors)
            //    .HasForeignKey(x => x.ActorId);

            //builder.HasOne(x => x.Movie)
            //    .WithMany(x => x.MoviesActors)
            //    .HasForeignKey(x => x.MovieId);

            #endregion

            builder.Property(prop => prop.Character)
                .HasMaxLength(150);
        }
    }
}
