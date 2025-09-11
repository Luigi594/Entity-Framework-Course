using static EFCoreCourse.Entities.Actors;
using static EFCoreCourse.Entities.MovieTheater;

namespace EFCoreCourse.Entities
{
    public class Movie
    {
        #region Properties

        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsOnDisplay { get; set; }
        public string PosterUrl { get; set; }

        #endregion

        #region Navs

        public ICollection<GenresDTO> Genres { get; set; }
        public ICollection<MovieTheaterRoom> MovieTheaterRooms { get; set; }
        public ICollection<MoviesActors> MoviesActors { get; set; }

        #endregion

        #region MovieDTOs

        public class MovieDTO
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public bool IsOnDisplay { get; set; }
            public List<GenresDTO> Genres { get; set; }
            public List<MoviesTheaterDTO> MoviesTheaters { get; set; }
            public List<ActorsDTO> Actors { get; set; }
        }


        #endregion
    }
}
