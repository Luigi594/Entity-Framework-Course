using EFCoreCourse.Utils;
using NetTopologySuite.Geometries;

namespace EFCoreCourse.Entities
{
    public class MovieTheater: Entity
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public MovieOffer MovieOffer { get; set; }

        #endregion

        #region Navs

        public ICollection<MovieTheaterRoom> MovieTheaterRooms { get; set; }

        public MovieTheater()
        {
            MovieTheaterRooms = new HashSet<MovieTheaterRoom>();
        }

        #endregion

        #region Methods

        public static MovieTheater Create(string name, double latitude,
            double longitude, List<MovieTheaterRoom> movieTheaterRooms)
        {
            var geometryFactory = new Utils.GeometryFactory();
            var location = geometryFactory.GetGeometryFromLocation(latitude, longitude);

            return new MovieTheater
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Name = name,
                Location = location,
                MovieTheaterRooms = movieTheaterRooms,
                CreatedAt = DateTime.Now
            };
        }

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
