using EFCoreCourse.Utils;

namespace EFCoreCourse.Entities
{
    // Person entity representing an individual with basic details
    // With the Messagess Entity I just want to apply how to handle 2 navigation properties
    // and their foreign keys
    public class Person: Entity
    {
        // I could omit this Id property if I want to inherit it from the base Entity class
        // but because I already have other entities that do not inherit from Entity 
        // I don't have idea if it would cause any problem

        #region Properties

        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime BirthDate { get; protected set; }

        #endregion

        #region  Navs

        public ICollection<Messages> SentMessages { get; set; }
        public ICollection<Messages> ReceivedMessages { get; set; }

        public Person()
        {
            SentMessages = new HashSet<Messages>();
            ReceivedMessages = new HashSet<Messages>();
        }

        #endregion

        #region Methods

        protected Person(string name, string lastName, DateTime birthDate)
        {
            Id = IdentityGenerator.GenerateNewIdentity();
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
        }

        public static Person Create(string name, string lastName, DateTime birthDate)
        {
            // Calling the protected constructor
            return new Person(name, lastName, birthDate);
        }

        #endregion
    }
}
