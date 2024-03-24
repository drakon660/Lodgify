using System;
using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
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

        Auditorium auditorium = Auditorium.Create("Einstein", Utils.Generate(3,8));
        
        context.Auditoriums.Add(auditorium);
        
        Movie movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06));
        
        context.Movies.Add(movie);
        
        context.SaveChanges();
    }
}