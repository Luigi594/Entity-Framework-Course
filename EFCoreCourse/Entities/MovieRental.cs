using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class MovieRental : Entity
    {
        #region Properties

        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal RentalPrice { get; set; }

        #endregion

        #region Foreign Keys

        public Guid RentalTransactionId { get; set; }
        public RentalTransaction RentalTransaction { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        #endregion

        #region Methods

        public static MovieRental Create(Guid rentalTransactionId, Guid movieId, DateTime rentalDate, decimal rentalPrice)
        {
            var movieRental = new MovieRental
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                RentalDate = rentalDate,
                RentalTransactionId = rentalTransactionId,
                MovieId = movieId,
                RentalPrice = rentalPrice,
                CreatedAt = DateTime.Now
            };
            return movieRental;
        }

        #endregion
    }
}
