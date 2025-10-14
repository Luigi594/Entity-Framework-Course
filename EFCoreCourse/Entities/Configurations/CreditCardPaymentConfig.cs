using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class CreditCardPaymentConfig : IEntityTypeConfiguration<CreditCardPayment>
    {
        public void Configure(EntityTypeBuilder<CreditCardPayment> builder)
        {
            builder.Property(x => x.LastFourDigits).HasColumnType("char(4)").IsRequired();
            builder.Property(x => x.CardHolderName).HasMaxLength(150);
        }
    }
}
