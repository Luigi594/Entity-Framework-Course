using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class MovieOfferConfig : IEntityTypeConfiguration<MovieOffer>
    {
        public void Configure(EntityTypeBuilder<MovieOffer> builder)
        {
            builder.Property(prop => prop.DiscountPercentage)
                .HasPrecision(5, 2);

            builder.Property(prop => prop.StartDate)
                .HasColumnType("date");

            builder.Property(prop => prop.EndDate)
                .HasColumnType("date");
        }
    }
}
