using System.Linq.Expressions;
using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface IShowtimesRepository
    {
        Task<Showtime> CreateShowtime(Showtime showtime, CancellationToken cancel);

        Task<Showtime> GetByMovieTitleAsync(string movieTitle);
        Task<Showtime> UpdateShowTime(Showtime showtime);
        
        Task<IEnumerable<Showtime>> GetAllAsync(Expression<Func<Showtime, bool>> filter, CancellationToken cancel);
        Task<Showtime> GetWithMoviesByIdAsync(int id, CancellationToken cancel);
        Task<Showtime> GetWithTicketsByIdAsync(int id, CancellationToken cancel);
    }
}