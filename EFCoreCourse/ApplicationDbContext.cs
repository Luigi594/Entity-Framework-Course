using EFCoreCourse.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse
{
    public class ApplicationDbContext(DbContextOptions options): DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Gender Configuration

            modelBuilder.Entity<Gender>().Property(prop => prop.Description)
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

            modelBuilder.Entity<MovieTheater>().Property(prop => prop.Price)
                .HasPrecision(9, 2);

            #endregion
        }

        public DbSet<Gender> Gender { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<MovieTheater> MovieTheater { get; set; }

    }
}
