namespace EFCoreCourse.Entities
{
    public class Actors
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }

        #endregion


        #region Navs

        public ICollection<MoviesActors> MoviesActors { get; set; }

        #endregion


        #region Actors DTOs

        public class ActorsDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
        }

        #endregion
    }
}
