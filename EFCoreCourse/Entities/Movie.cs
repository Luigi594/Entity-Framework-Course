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

        public ICollection<Genres> Genres { get; set; }
        public ICollection<MovieTheaterRoom> MovieTheaterRooms { get; set; }
        public ICollection<MoviesActors> MoviesActors { get; set; }

        #endregion
    }
}
