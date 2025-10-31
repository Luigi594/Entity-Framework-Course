using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class PayPalPayment : Payment
    {
        #region Properties

        public string Email { get; set; }
        public string ReferenceCode { get; set; }

        #endregion

        #region Methods

        private PayPalPayment(string email, decimal amount): base(amount)
        {
            Email = email;
            ReferenceCode = CodeGenerator.Generate(email);
        }

        public static PayPalPayment Create(string email, decimal amount)
        {
            if (!EmailValidator.IsValidEmail(email))
                throw new ArgumentException("The Email is not valid!");

            return new PayPalPayment(email, amount);
        }

        #endregion

        #region DTOs

        public class Vm
        {
            public string Email { get; set; }
        }

        #endregion
    }
}
