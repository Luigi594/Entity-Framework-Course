using EFCoreCourse.Entities;
using EFCoreCourse.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCoreCourse
{
    public class ApplicationDbContext(DbContextOptions options): DbContext(options)
    {
        // If I wanted to have configured certain properties by convention, I would do it here.
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the current assembly
            // Identify all classes that implement IEntityTypeConfiguration<T> and apply them here
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        #region Table Names

        public DbSet<GenresDTO> Genres { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheater { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieOffer> MovieOffer { get; set; }
        public DbSet<MovieTheaterRoom> MovieTheaterRoom { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }

        #endregion

    }
}
