using EFCoreCourse.Entities.Enums;
using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class MovieTheaterRoom: Entity
    {
        #region Properties

        public Guid Id { get; set; }
        public Guid MovieTheaterId { get; set; }
        public MovieTheater MovieTheater { get; set; }
        public decimal Price { get; set; }
        public MovieTheaterRoomType MovieTheaterRoomType { get; set; }
        public Currency Currency { get; set; }

        #endregion


        #region Navs

        public ICollection<Movie> Movies { get; set; }

        public MovieTheaterRoom()
        {
            Movies = new HashSet<Movie>();
        }

        #endregion

        #region Methods

        public static MovieTheaterRoom Create(decimal price, 
            MovieTheaterRoomType movieTheaterRoomType)
        {
            if(!Enum.IsDefined(movieTheaterRoomType))
                throw new Exception("The movie theater room type is not valid.");

            return new MovieTheaterRoom
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Price = price,
                MovieTheaterRoomType = movieTheaterRoomType
            };
        }

        #endregion
    }
}
