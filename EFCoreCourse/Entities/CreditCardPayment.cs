namespace EFCoreCourse.Entities
{
    public class CreditCardPayment : Payment
    {
        #region Properties

        public string LastFourDigits { get; set; }
        public string CardHolderName { get; set; }

        #endregion

        #region Methods

        public static CreditCardPayment Create(string lastFourDigits, string cardHolderName)
        {
            return new CreditCardPayment
            {
                LastFourDigits = lastFourDigits,
                CardHolderName = cardHolderName
            };
        }

        #endregion
    }
}
