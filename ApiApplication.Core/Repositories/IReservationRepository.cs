using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories;

public interface IReservationRepository
{
    public Task<IReadOnlyList<Reservation>> GetAllByShowtimeAsync(Showtime showtime);
    public Task<Reservation> GetByIdAsync(Guid id);
}