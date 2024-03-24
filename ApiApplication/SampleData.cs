using System;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
        
        auditorium.SetSeats(Utils.Generate(3,8));
        
        Auditorium auditorium2 = Auditorium.Create("Viper");
        
        auditorium.SetSeats(Utils.Generate(10,20));
        
        Movie movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06),96);
        
        Movie movie2 = Movie.Create("Interstellar", "tt0816692", "Matthew McConaughey, Anne Hathaway, Jessica Chastain",
            new DateTime(2014, 07, 06),209);
        
        Movie movie3 = Movie.Create("The Green Mile", "tt0120689", "Tom Hanks, Michael Clarke Duncan, David Morse",
            new DateTime(1999, 03, 04),189);
        
        context.Auditoriums.AddRange(auditorium, auditorium2);
        context.Movies.AddRange(movie, movie2, movie3);
        
        context.SaveChanges();

        var list = context.Auditoriums.Where(x => x.Id == 1).Include(x => x.Seats).ToList();
    }
}