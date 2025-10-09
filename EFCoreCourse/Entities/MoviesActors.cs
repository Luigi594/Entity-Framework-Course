namespace EFCoreCourse.Entities
{
    public class MoviesActors: Entity
    {
        #region Properties

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid ActorId { get; set; }
        public Actors Actor { get; set; }
        public int Order { get; set; }
        public string Character { get; set; }

        #endregion

        #region Methods

        public static MoviesActors Create(Guid movieId, Guid actorId, string character, int order)
        {   
            return new MoviesActors
            {
                MovieId = movieId,
                ActorId = actorId,
                Character = character,
                Order = order,
                CreatedAt = DateTime.Now
            };
        }

        #endregion
    }
}
