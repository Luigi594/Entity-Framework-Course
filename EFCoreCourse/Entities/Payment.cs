namespace EFCoreCourse.Entities
{
    public abstract class Payment : Entity
    {
        #region Properties

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        //public PaymentType PaymentType { get; set; }

        #endregion
    }
}
