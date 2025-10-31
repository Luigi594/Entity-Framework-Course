using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class CreditCardPayment : Payment
    {
        #region Properties

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }

        #endregion

        #region Methods

        public static CreditCardPayment Create(string cardNumber, string cardHolderName, decimal amount)
        {
            // It should bring the Id and Amount from the parent clase, from the base constructor
            // I'll fix this later, the same thing for PayPal and Cash class
            return new CreditCardPayment
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                CardNumber = cardNumber,
                CardHolderName = cardHolderName,
                Amount = amount,
                CreatedAt = DateTime.Now,
            };
        }

        #endregion

        #region DTOs

        public class Vm
        {
            public string CardNumber { get; set; }
            public string CardHolderName { get; set; }
        }

        #endregion
    }
}
