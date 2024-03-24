using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure.Repositories;

public class AuditoriumsRepository : IAuditoriumsRepository
{
    private readonly CinemaContext _context;

    public AuditoriumsRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<Auditorium> GetAsync(int auditoriumId, CancellationToken cancel)
    {
        return await _context.Auditoriums
            .Include(x => x.Seats)
            .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancel);
    }

    public async Task<Auditorium> GetById(int auditoriumId, CancellationToken cancellationToken)
    {
        return await _context.Auditoriums.FindAsync(auditoriumId, cancellationToken);
    }
}