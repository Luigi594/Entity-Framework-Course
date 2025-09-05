using NetTopologySuite.Geometries;

namespace EFCoreCourse.Entities
{
    public class MovieTheater
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; } 
        public MovieOffer MovieOffer { get; set; }

        #endregion

        #region Navs

        public ICollection<MovieTheaterRoom> MovieTheaterRooms { get; set; }

        #endregion

        #region MovieTheater DTOs
        
        public class MoviesTheaterDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        #endregion
    }
}
