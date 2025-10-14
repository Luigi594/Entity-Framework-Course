using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            //builder.HasDiscriminator(x => x.PaymentType)
            //    .HasValue<PayPalPayment>(PaymentType.PayPal)
            //    .HasValue<CreditCardPayment>(PaymentType.CreditCard);

            builder.Property(x => x.Amount).HasPrecision(18, 2);
        }
    }
}
