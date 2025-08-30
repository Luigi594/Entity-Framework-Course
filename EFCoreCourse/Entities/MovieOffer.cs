namespace EFCoreCourse.Entities
{
    public class MovieOffer
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid MovieTheaterId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion
    }
}
