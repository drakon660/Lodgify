using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
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

    public async Task UpdateShowtime(Showtime showtime, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Showtime> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.Showtimes.Include(x => x.Auditorium).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Showtime>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Showtimes.Include(x=>x.Auditorium)
            .Include(x=>x.Movie).ToListAsync(cancellationToken);
    }
}