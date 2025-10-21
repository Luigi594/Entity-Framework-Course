using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class RentalTransaction: Entity
    {
        #region Properties

        public string ReferenceCode { get; set; }

        #endregion

        #region Foreign Keys

        public Guid CustomerId { get; set; }
        public Customer Custommer { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }

        #endregion

        #region Navs

        public ICollection<MovieRental> MovieRentals { get; set; }

        public RentalTransaction()
        {
            MovieRentals = new HashSet<MovieRental>();
        }

        #endregion

        #region Methods

        public static RentalTransaction Create(Guid customerId, Guid paymentId)
        {
            var referenceCode = CodeGenerator.Generate();

            var rentalTransaction = new RentalTransaction
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                ReferenceCode = $"RT-{referenceCode}",
                CustomerId = customerId,
                PaymentId = paymentId,
                CreatedAt = DateTime.Now
            };

            return rentalTransaction;
        }

        #endregion
    }
}
