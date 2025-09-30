using EFCoreCourse.Utils;
using static EFCoreCourse.Entities.Actors;
using static EFCoreCourse.Entities.MovieTheater;

namespace EFCoreCourse.Entities
{
    public class Movie: Entity
    {
        #region Properties

        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsOnDisplay { get; set; }
        public string PosterUrl { get; set; }

        #endregion

        #region Navs

        public ICollection<Genres> Genres { get; set; }
        public ICollection<MovieTheaterRoom> MovieTheaterRooms { get; set; }
        public ICollection<MoviesActors> MoviesActors { get; set; }

        public Movie()
        {
            Genres = new HashSet<Genres>();
            MovieTheaterRooms = new HashSet<MovieTheaterRoom>();
            MoviesActors = new HashSet<MoviesActors>();
        }

        #endregion

        #region Methods

        public static Movie Create(string title, DateTime releaseDate, bool isOnDisplay,
            string posterUrl)
        {
            var movie = new Movie
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Title = title,
                ReleaseDate = releaseDate,
                IsOnDisplay = isOnDisplay,
                PosterUrl = posterUrl,
            };

            return movie;
        }

        #endregion

        #region MovieDTOs

        public class MovieDTOBase
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public DateTime ReleaseDate { get; set; }
            public bool IsOnDisplay { get; set; }
            public string PosterUrl { get; set; }
        }

        public class MovieDTO
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public bool IsOnDisplay { get; set; }
            public List<Genres.GenreDTO> Genres { get; set; }
            public List<MoviesTheaterDTO> MoviesTheaters { get; set; }
            public List<ActorsDTO> Actors { get; set; }
        }


        #endregion
    }
}
