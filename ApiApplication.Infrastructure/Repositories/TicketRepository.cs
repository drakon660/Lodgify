using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CinemaContext _cinemaContext;

    public TicketRepository(CinemaContext cinemaContext)
    {
        _cinemaContext = cinemaContext;
    }
    
    public async Task<IEnumerable<Ticket>> GetAll(CancellationToken cancellationToken)
    {
        return await _cinemaContext.Tickets
            .Include(x => x.Showtime)
            .ThenInclude(x=>x.Movie)
            .Include(x => x.Seats)
            .ThenInclude(x=>x.Auditorium)            
            .ToListAsync(cancellationToken);
    }

    public async Task Save(Ticket ticket, CancellationToken cancellationToken)
    {
        _cinemaContext.Tickets.Add(ticket);
        await _cinemaContext.SaveChangesAsync(cancellationToken);
    }
}