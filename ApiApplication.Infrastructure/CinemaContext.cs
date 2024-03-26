using ApiApplication.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure
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
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auditorium>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasMany(x => x.Seats).WithOne(x=>x.Auditorium).Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
                build.HasMany(entry => entry.Showtimes).WithOne(x=>x.Auditorium).Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<Seat>(build =>
            {
                build.HasKey(x => x.Id);
                build.Property(x => x.Id).ValueGeneratedOnAdd();
                build.HasOne(entry => entry.Auditorium).WithMany(entry => entry.Seats);
                build.OwnsOne(x => x.Position);
            });
            
            modelBuilder.Entity<Showtime>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne(x => x.Auditorium).WithMany(x => x.Showtimes);
                build.HasMany(entry => entry.Tickets).WithOne(entry => entry.Showtime);
            });
            
            modelBuilder.Entity<Movie>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            });
            
            modelBuilder.Entity<Ticket>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne(entry => entry.Showtime);
            });

            modelBuilder.Entity<Reservation>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasMany(entry => entry.Seats);
                build.HasOne(entry => entry.Showtime)
                    .WithMany(x=>x.Reservations);
            });
        }
    }
}
