using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class CashPaymentConfig : IEntityTypeConfiguration<CashPayment>
    {
        public void Configure(EntityTypeBuilder<CashPayment> builder)
        {
            builder.Property(x => x.ReceivedBy).HasMaxLength(150);
            
            builder.Property(x => x.ChangeAmount).HasPrecision(18, 2);
        }
    }

}
