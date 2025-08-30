using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class MovieTheaterRoomConfig : IEntityTypeConfiguration<MovieTheaterRoom>
    {
        public void Configure(EntityTypeBuilder<MovieTheaterRoom> builder)
        {
            builder.Property(prop => prop.Price)
                .HasPrecision(9, 2);
        }
    }
}
