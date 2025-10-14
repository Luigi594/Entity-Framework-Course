using EFCoreCourse.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace EFCoreCourse
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        // If I wanted to have configured certain properties by convention, I would do it here.
        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        //}

        #region Table Names

        public DbSet<Genres> Genres { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheater { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieOffer> MovieOffer { get; set; }
        public DbSet<MovieTheaterRoom> MovieTheaterRoom { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Payment> Payment { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Global Filter for Soft Delete

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // If the entity inherits from our base class "Entity"  
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    // Create the parameter for the lambda expression (e.g., "e") 
                    // of the correct type (e.g., Actors, Genres, etc.) 
                    var parameter = Expression.Parameter(entityType.ClrType, "e");

                    //   (ej: e.IsSoftDeleted)  
                    var property = Expression.Property(parameter, nameof(Entity.IsSoftDeleted));

                    // Creates the body of the expression, which is the negation: !e.IsSoftDeleted
                    var body = Expression.Not(property);

                    // Create the lambda expression: e => !e.IsSoftDeleted
                    var lambda = Expression.Lambda(body, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);

                    // Configure string properties that contain "Url" in their name
                    foreach (var prop in entityType.GetProperties())
                    {
                        if (prop.ClrType == typeof(string) &&
                            prop.Name.Contains("Url", StringComparison.CurrentCultureIgnoreCase))
                        {
                            prop.SetIsUnicode(false);
                            prop.SetMaxLength(500);
                        }
                    }
                }
            }

            #endregion

            // Apply all configurations from the current assembly
            // Identify all classes that implement IEntityTypeConfiguration<T> and apply them here
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
