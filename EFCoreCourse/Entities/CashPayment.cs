using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class CashPayment : Payment
    {
        #region Properties

        public string? ReceivedBy { get; set; }
        public bool ChangeGiven { get; set; }
        public decimal? ChangeAmount { get; set; }

        #endregion

        #region Methods

        private CashPayment(string receivedBy, decimal amount): base(amount)
        {
            ReceivedBy = receivedBy ?? "";
        }

        public static CashPayment Create(string receivedBy, decimal amount)
        {
            return new CashPayment(receivedBy, amount);
        }

        #endregion

        #region  DTOs

        public class Vm
        {
            public string ReceivedBy { get; set; }
            public bool ChangeGiven { get; set; }
            public decimal? ChangeAmount { get; set; }
        }

        #endregion
    }
}
