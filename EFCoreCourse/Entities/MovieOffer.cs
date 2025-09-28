using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class MovieOffer
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid MovieTheaterId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion

        #region Methods 

        public static MovieOffer Create(decimal discountPercentage, 
            DateTime startDate, DateTime endDate)
        {
            if(endDate < startDate)
                throw new Exception("The end date must be greater than or equal to the start date.");

            return new MovieOffer
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                DiscountPercentage = discountPercentage,
                StartDate = startDate,
                EndDate = endDate
            };
        }

        #endregion
    }
}
