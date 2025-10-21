using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class Customer: Person
    {
        public string CustomerCode { get; protected set; }

        #region Methods

        protected Customer(string name, string lastname, DateTime birthDate, string customerCode)
            : base(name, lastname, birthDate)
        {
            // This constructor would only care about setting the CustomerCode property
            CustomerCode = customerCode;
        }

        public static Customer Create(string name, string lastName, DateTime birthDate, string customerCode)
        {
            return new Customer(name, lastName, birthDate, customerCode);
        }

        #endregion
    }
}
