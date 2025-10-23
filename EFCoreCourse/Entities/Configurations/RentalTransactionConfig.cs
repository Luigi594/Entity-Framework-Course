using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class RentalTransactionConfig : IEntityTypeConfiguration<RentalTransaction>
    {
        public void Configure(EntityTypeBuilder<RentalTransaction> builder)
        {
            builder.HasOne(x => x.Payment)
                .WithOne()
                .HasForeignKey<RentalTransaction>(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(rt => rt.MovieRentals)
                .WithOne(mr => mr.RentalTransaction)
                .HasForeignKey(mr => mr.RentalTransactionId);

            builder.Property(x => x.ReferenceCode).HasMaxLength(50).IsRequired();
        }
    }
}
