using EFCoreCourse.Utils;

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

        public Actors()
        {
            MoviesActors = new HashSet<MoviesActors>();
        }

        #endregion

        #region Methods

        public static Actors Create(string name, string lastName, string biography, DateTime birthDate)
        {
            return new Actors
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Name = name,
                LastName = lastName,
                Biography = biography,
                BirthDate = birthDate
            };
        }

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
