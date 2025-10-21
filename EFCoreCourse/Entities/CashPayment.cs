namespace EFCoreCourse.Entities
{
    public class CashPayment: Payment
    {
        #region Properties

        public string? ReceivedBy { get; set; }
        public bool ChangeGiven { get; set; }
        public decimal? ChangeAmount { get; set; }

        #endregion
    }
}
