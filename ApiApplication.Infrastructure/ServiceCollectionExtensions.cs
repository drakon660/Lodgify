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
        serviceCollection.AddTransient<IShowtimeRepository, ShowtimeRepository>();
        serviceCollection.AddTransient<IMovieRepository, MovieRepository>();
        serviceCollection.AddTransient<IAuditoriumsRepository, AuditoriumsRepository>();
        serviceCollection.AddTransient<IReservationRepository, ReservationRepository>();
        serviceCollection.AddTransient<ITicketRepository, TicketRepository>();
        
        serviceCollection.AddTransient<IShowtimeService, ShowtimeService>();
        serviceCollection.AddTransient<IMovieService, MovieService>();
        serviceCollection.AddTransient<IAuditoriumService, AuditoriumService>();
        
        return serviceCollection;
    }
}