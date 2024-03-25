using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories;

public interface IReservationRepository
{
    public Task<IEnumerable<Reservation>> GetByAll(CancellationToken cancellationToken);
    public Task<Reservation> GetById(Guid id, CancellationToken cancellationToken);

    public Task Update(Reservation reservation, CancellationToken cancellationToken);
}