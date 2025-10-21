namespace EFCoreCourse.Entities
{
    // We obviously could have an API Payment Gateway, this is just a demo.
    public abstract class Payment : Entity
    {
        #region Properties

        public decimal Amount { get; set; }

        #endregion
    }
}
