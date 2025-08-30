using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class GenresConfig : IEntityTypeConfiguration<Genres>
    {
        public void Configure(EntityTypeBuilder<Genres> builder)
        {
            builder.Property(prop => prop.Description)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
