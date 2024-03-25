using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure.Repositories;

public class AuditoriumsRepository : IAuditoriumsRepository
{
    private readonly CinemaContext _context;

    public AuditoriumsRepository(CinemaContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Auditorium>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Auditoriums.Include(x => x.Seats)
            .Include(x => x.Showtimes).ToListAsync(cancellationToken);
    }

    public async Task<Auditorium> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.Auditoriums.Include(x => x.Seats).Include(x => x.Showtimes)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}