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

        private CreditCardPayment(string cardNumber, string cardHolderName, decimal amount)
            : base(amount)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
        }

        public static CreditCardPayment Create(string cardNumber, string cardHolderName, decimal amount)
        {
            ValidateCardNumber(cardNumber);
            ValidateCardHolderName(cardHolderName);
            ValidateAmount(amount);

            return new CreditCardPayment(cardNumber, cardHolderName, amount);
        }

        private static void ValidateCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number is required.");

            if (cardNumber.Length < 4)
                throw new ArgumentException("Card number must be at least 4 digits.");

            if (cardNumber.Length > 16)
                throw new ArgumentException("Card number must not exceed 16 digits.");

            if (!cardNumber.All(char.IsDigit))
                throw new ArgumentException("Card number must contain only digits.");
        }

        private static void ValidateCardHolderName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Card holder name is required.");

            if (name.Length > 100)
                throw new ArgumentException("Card holder name must not exceed 100 characters.");
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
