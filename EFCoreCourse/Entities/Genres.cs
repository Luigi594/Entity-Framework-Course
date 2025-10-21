using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class Genres: Entity
    {
        public string Description { get; set; }

        #region Navs

        public ICollection<Movie> Movies { get; set; }

        public Genres()
        {
            Movies = new HashSet<Movie>();
        }

        #endregion

        #region Methods

        public static Genres New(string description)
        {
            var genre = new Genres
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Description = description,
                CreatedAt = DateTime.Now
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
