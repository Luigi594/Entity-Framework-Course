using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreCourse.Utils
{
    public class GeometryFactory
    {
        public Point GetGeometryFromLocation(double latitude, double longitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var location = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
            return location;
        }
    }
}
