using NetTopologySuite.Geometries;

namespace EFCoreCourse.Entities
{
    public class MovieTheater
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Point Location { get; set; } 

    }
}
