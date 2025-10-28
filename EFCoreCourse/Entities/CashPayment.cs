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

        public static CashPayment Create(string receivedBy, decimal amount)
        {
            return new CashPayment
            {
                ReceivedBy = receivedBy ?? "",
                Amount = amount,
                CreatedAt = DateTime.Now,
            };
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
