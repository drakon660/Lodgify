using System.Linq.Expressions;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure.Repositories;

public class ShowtimeRepository : IShowtimeRepository
{
    private readonly CinemaContext _context;

    public ShowtimeRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task CreateShowtime(Showtime showtime, CancellationToken cancellationToken)
    {
        _context.Showtimes.Add(showtime);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Showtime> GetByMovieTitleAsync(string movieTitle)
    {
        throw new NotImplementedException();
    }

    public Task<Showtime> UpdateShowTime(Showtime showtime)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Showtime>> GetAllAsync(Expression<Func<Showtime, bool>> filter, CancellationToken cancel)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Showtime>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Showtimes.Include(x => x.Auditorium).Include(x => x.Movie).ToListAsync(cancellationToken);
    }

    public Task<Showtime> GetWithMoviesByIdAsync(int id, CancellationToken cancel)
    {
        throw new NotImplementedException();
    }

    public Task<Showtime> GetWithTicketsByIdAsync(int id, CancellationToken cancel)
    {
        throw new NotImplementedException();
    }
}