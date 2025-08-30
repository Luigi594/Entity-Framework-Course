namespace EFCoreCourse.Entities
{
    public class Genres
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        #region Navs

        public ICollection<Movie> Movies { get; set; }

        #endregion
    }
}
