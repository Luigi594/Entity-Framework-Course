using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class PayPalPayment : Payment
    {
        #region Properties

        public string Email { get; set; }

        #endregion

        #region Methods

        public static PayPalPayment Create(string email, decimal amount)
        {
            return new PayPalPayment
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Email = email,
                Amount = amount,
                CreatedAt = DateTime.Now,
            };
        }

        #endregion
    }
}
