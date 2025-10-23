using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class PayPalPaymentConfig : IEntityTypeConfiguration<PayPalPayment>
    {
        public void Configure(EntityTypeBuilder<PayPalPayment> builder)
        {
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.ReferenceCode).HasMaxLength(50).IsRequired();
        }
    }
}
