using System;
using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.ValueObjects;
using ApiApplication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication;

public class SampleData
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CinemaContext>();
        context.Database.EnsureCreated();
        
        Auditorium auditorium = Auditorium.Create("Einstein");
        
        auditorium.SetSeats(Utils.Generate(2,3));
        
        Auditorium auditorium3 = Auditorium.Create("E.T");
        
        auditorium3.SetSeats(Utils.Generate(5,5));
        
        Auditorium auditorium2 = Auditorium.Create("Viper");
        
        auditorium2.SetSeats(Utils.Generate(10,20));
        
        Movie movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06),96);
        
        Movie movie2 = Movie.Create("Interstellar", "tt0816692", "Matthew McConaughey, Anne Hathaway, Jessica Chastain",
            new DateTime(2014, 07, 06),209);
        
        Movie movie3 = Movie.Create("The Green Mile", "tt0120689", "Tom Hanks, Michael Clarke Duncan, David Morse",
            new DateTime(1999, 03, 04),189);
        
        var showtime = Showtime.Create(movie, new DateTime(2020, 03, 03, 11, 00, 00, DateTimeKind.Utc), auditorium);
        var showtime3 = Showtime.Create(movie3, new DateTime(2020, 03, 03, 11, 00, 00, DateTimeKind.Utc), auditorium3);
        
        var currentDate = DateTime.UtcNow;

        var reservation = showtime.Value.ReserveSeats([Position.Create(2,3)], DateTime.UtcNow);
        
        var ticket = Ticket.Create(reservation.Value, currentDate.AddMinutes(2));
        
        context.Auditoriums.AddRange(auditorium, auditorium2);
        context.Movies.AddRange(movie, movie2, movie3);
        context.Showtimes.AddRange(showtime.Value, showtime3.Value);
        context.Reservations.Add(reservation.Value);
        context.Tickets.Add(ticket.Value);
        
        context.SaveChanges();
    }
}