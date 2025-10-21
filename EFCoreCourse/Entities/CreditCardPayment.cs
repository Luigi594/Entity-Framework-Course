namespace EFCoreCourse.Entities
{
    public class CreditCardPayment : Payment
    {
        #region Properties

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }

        #endregion

        #region Methods

        public static CreditCardPayment Create(string lastFourDigits, string cardHolderName, decimal amount)
        {
            return new CreditCardPayment
            {
                CardNumber = lastFourDigits,
                CardHolderName = cardHolderName,
                Amount = amount,
                CreatedAt = DateTime.Now,
            };
        }

        #endregion
    }
}
