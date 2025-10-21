using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    public class Actors: Entity
    {
        #region Properties

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public string PictureUrl { get; set; }

        // This property is not mapped to the database, it's a calculated field.
        public int? Age {
            get
            {
                if (BirthDate == DateTime.MinValue) return null;

                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;

                // If the birthday hasn't occurred yet this year, subtract one from age.
                if (BirthDate.Date > today.AddYears(-age)) age--;

                return age;
            }
        }
        #endregion


        #region Navs

        public ICollection<MoviesActors> MoviesActors { get; set; }

        public Actors()
        {
            MoviesActors = new HashSet<MoviesActors>();
        }

        #endregion

        #region Methods

        public static Actors Create(string name, string lastName, string biography, DateTime birthDate, string pictureUrl)
        {
            return new Actors
            {
                Id = IdentityGenerator.GenerateNewIdentity(),
                Name = name,
                LastName = lastName,
                Biography = biography,
                BirthDate = birthDate,
                PictureUrl = pictureUrl,
                CreatedAt = DateTime.Now,
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
