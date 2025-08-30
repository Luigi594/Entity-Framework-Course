using EFCoreCourse.Entities;
using EFCoreCourse.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse
{
    public class ApplicationDbContext(DbContextOptions options): DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Genres Configuration

            modelBuilder.Entity<Genres>().Property(prop => prop.Description)
                .HasMaxLength(150)
                .IsRequired();

            #endregion

            #region Actors Configuration

            modelBuilder.Entity<Actors>().Property(prop => prop.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Actors>().Property(prop => prop.LastName)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Actors>().Property(prop => prop.BirthDate)
                .HasColumnType("date")
                .HasDefaultValue(new DateTime(1900, 1, 1));

            #endregion

            #region MovieTheater Configuration

            modelBuilder.Entity<MovieTheater>().Property(prop => prop.Name)
                .HasMaxLength(150)
                .IsRequired();

            #endregion

            #region Movie Configuration
            modelBuilder.Entity<Movie>().Property(prop => prop.Title)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Movie>().Property(prop => prop.ReleaseDate)
                .HasColumnType("date");

            modelBuilder.Entity<Movie>().Property(prop => prop.PosterUrl)
                .HasMaxLength(500)
                .IsUnicode(false); // Non-Unicode string (VARCHAR)
            #endregion

            #region MovieOffer Configuration

            modelBuilder.Entity<MovieOffer>().Property(prop => prop.DiscountPercentage)
                .HasPrecision(5, 2);

            modelBuilder.Entity<MovieOffer>().Property(prop => prop.StartDate)
                .HasColumnType("date");

            modelBuilder.Entity<MovieOffer>().Property(prop => prop.EndDate)
                .HasColumnType("date");


            #endregion

            #region MovieTheaterRoom Configuration

            modelBuilder.Entity<MovieTheaterRoom>().Property(prop => prop.Price)
                .HasPrecision(9, 2);

            #endregion

            #region MoviesActors Configuration

            modelBuilder.Entity<MoviesActors>().HasKey(prop => new { prop.MovieId, prop.ActorId });

            modelBuilder.Entity<MoviesActors>().Property(prop => prop.Character)
                .HasMaxLength(150);

            #endregion


        }

        #region Table Names

        public DbSet<Genres> Genres { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheater { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieOffer> MovieOffer { get; set; }
        public DbSet<MovieTheaterRoom> MovieTheaterRoom { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }

        #endregion

    }
}
