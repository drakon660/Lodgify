using ApiApplication.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
            
        }

        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Auditorium>(build =>
            // {
            //     build.HasKey(entry => entry.Id);
            //     build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            //     build.HasMany(entry => entry.Showtimes).WithOne().HasForeignKey(entity => entity.AuditoriumId);
            // });
            //
            // modelBuilder.Entity<Seat>(build =>
            // {
            //     build.HasKey(entry => new { entry.AuditoriumId, Row = entry.RowNumber, entry.SeatNumber });
            //     build.HasOne(entry => entry.Auditorium).WithMany(entry => entry.Seats).HasForeignKey(entry => entry.AuditoriumId);
            // });
            //
            // modelBuilder.Entity<Showtime>(build =>
            // {
            //     build.HasKey(entry => entry.Id);
            //     build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            //     build.HasOne(entry => entry.Movie).WithMany(entry => entry.Showtimes);
            //     build.HasMany(entry => entry.Tickets).WithOne(entry => entry.Showtime).HasForeignKey(entry => entry.ShowtimeId);
            // });
            //
            // modelBuilder.Entity<Movie>(build =>
            // {
            //     build.HasKey(entry => entry.Id);
            //     build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            // });
            //
            // modelBuilder.Entity<Ticket>(build =>
            // {
            //     build.HasKey(entry => entry.Id);
            //     build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            // });
        }
    }
}
