using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class GenresConfig : IEntityTypeConfiguration<GenresDTO>
    {
        public void Configure(EntityTypeBuilder<GenresDTO> builder)
        {
            builder.Property(prop => prop.Description)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
