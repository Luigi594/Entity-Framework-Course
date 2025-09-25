using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class Genres
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        #region Navs

        public ICollection<Movie> Movies { get; set; }

        #endregion

        #region Methods

        public static Genres New(string description)
        {
            var genre = new Genres
            {
                Description = description,
                Id = IdentityGenerator.GenerateNewIdentity()
            };

            return genre;
        }

        #endregion

        #region GenreDTO
        public class GenreDTO
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
        }

        #endregion
    }
}
