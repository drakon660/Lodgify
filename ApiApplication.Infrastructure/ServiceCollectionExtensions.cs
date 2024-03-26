using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using ApiApplication.Infrastructure.Repositories;
using ApiApplication.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCinema(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IShowtimeRepository, ShowtimeRepository>();
        serviceCollection.AddScoped<IMovieRepository, MovieRepository>();
        serviceCollection.AddScoped<IAuditoriumsRepository, AuditoriumsRepository>();
        serviceCollection.AddScoped<IReservationRepository, ReservationRepository>();
        serviceCollection.AddScoped<ITicketRepository, TicketRepository>();
        
        serviceCollection.AddScoped<IShowtimeService, ShowtimeService>();
        serviceCollection.AddScoped<IMovieService, MovieService>();
        serviceCollection.AddScoped<IAuditoriumService, AuditoriumService>();
        
        return serviceCollection;
    }
}