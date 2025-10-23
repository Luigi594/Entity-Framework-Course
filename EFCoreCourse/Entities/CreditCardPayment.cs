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
            return new CreditCardPayment
            {
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
