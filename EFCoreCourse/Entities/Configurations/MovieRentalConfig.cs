using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCourse.Entities.Configurations
{
    public class MovieRentalConfig : IEntityTypeConfiguration<MovieRental>
    {
        public void Configure(EntityTypeBuilder<MovieRental> builder)
        {
            builder.Property(x => x.RentalPrice).HasPrecision(18, 2).IsRequired();
        }
    }
}
