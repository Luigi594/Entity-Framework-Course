namespace EFCoreCourse.Entities
{
    public class MoviesActors
    {
        #region Properties

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid ActorId { get; set; }
        public Actors Actor { get; set; }
        public int Order { get; set; }
        public string Character { get; set; }

        #endregion
    }
}
