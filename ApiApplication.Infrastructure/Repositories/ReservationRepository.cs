using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly CinemaContext _cinemaContext;

    public ReservationRepository(CinemaContext cinemaContext)
    {
        _cinemaContext = cinemaContext;
    }

    public async Task<IEnumerable<Reservation>> GetByAll(CancellationToken cancellationToken)
    {
        return await _cinemaContext.Reservations
            .Include(x => x.Showtime)
            .ThenInclude(x=>x.Movie)
            .Include(x=>x.Seats)
            .ThenInclude(x=>x.Auditorium)
            .ToListAsync(cancellationToken);
    }

    public async Task<Reservation> GetById(Guid id, CancellationToken cancellationToken)
    {
        var reservation = await _cinemaContext.Reservations
            .Include(x=>x.Seats)
            .ThenInclude(x=>x.Auditorium)
            .Include(x=>x.Showtime)
            .ThenInclude(x=>x.Movie)
            .Where(reservation => reservation.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return reservation;
    }

    public async Task Update(Reservation reservation, CancellationToken cancellationToken)
    {
        _cinemaContext.Reservations.Add(reservation);
        await _cinemaContext.SaveChangesAsync(cancellationToken);
    }
}