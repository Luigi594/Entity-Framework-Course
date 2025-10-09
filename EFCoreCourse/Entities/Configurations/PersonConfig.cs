using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
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

            builder.HasMany(m => m.SentMessages)
                .WithOne(p => p.Sender)
                .HasForeignKey(p => p.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.ReceivedMessages)
                .WithOne(p => p.Receiver)
                .HasForeignKey(p => p.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
