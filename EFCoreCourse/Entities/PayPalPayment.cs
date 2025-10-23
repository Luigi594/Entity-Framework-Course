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

        public static PayPalPayment Create(string email, decimal amount)
        {
            var referenceCode = CodeGenerator.Generate(email);

            return new PayPalPayment
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Email = email,
                Amount = amount,
                ReferenceCode = referenceCode,
                CreatedAt = DateTime.Now,
            };
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
