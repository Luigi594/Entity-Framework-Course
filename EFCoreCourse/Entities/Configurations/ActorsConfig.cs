using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreCourse.Entities.Configurations
{
    public class ActorsConfig : IEntityTypeConfiguration<Actors>
    {
        public void Configure(EntityTypeBuilder<Actors> builder)
        {
            builder.Property(prop => prop.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(prop => prop.LastName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(prop => prop.BirthDate)
                .HasColumnType("date")
                .HasDefaultValue(new DateTime(1900, 1, 1));

            // Do not map the Age property to any column in the database
            builder.Ignore(x => x.Age);
        }
    }
}
