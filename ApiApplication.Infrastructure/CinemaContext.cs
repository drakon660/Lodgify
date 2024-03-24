﻿using ApiApplication.Core.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auditorium>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasMany(x => x.Seats).WithOne(x=>x.Auditorium).Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
                //build.HasMany(entry => entry.Showtimes).WithOne().HasForeignKey(entity => entity.AuditoriumId);
            });

            modelBuilder.Entity<Seat>(build =>
            {
                build.HasKey(x => x.Id);
                build.Property(x => x.Id).ValueGeneratedOnAdd();
                build.HasOne(entry => entry.Auditorium).WithMany(entry => entry.Seats);
                build.ComplexProperty(x => x.Position).IsRequired();
            });
            
            modelBuilder.Entity<Showtime>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne(x => x.Auditorium).WithMany(x => x.Showtimes).HasForeignKey(x=>x.AuditoriumId);
                build.HasOne(entry => entry.Movie).WithMany(entry => entry.Showtimes);
                build.HasMany(entry => entry.Tickets).WithOne(entry => entry.Showtime).HasForeignKey(entry => entry.ShowtimeId);
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
            });
        }
    }
}
