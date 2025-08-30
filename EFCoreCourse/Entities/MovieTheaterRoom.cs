using EFCoreCourse.Entities.Enums;

namespace EFCoreCourse.Entities
{
    public class MovieTheaterRoom
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid MovieTheaterId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public decimal Price { get; set; }
        public MovieTheaterRoomType MovieTheaterRoomType { get; set; }

        #endregion


        #region Navs

        public ICollection<Movie> Movies { get; set; }

        #endregion
    }
}
