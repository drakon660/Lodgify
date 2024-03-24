using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface IShowtimeRepository
    {
        Task CreateShowtime(Showtime showtime, CancellationToken cancel);
        Task UpdateShowtime(Showtime showtime, CancellationToken cancel);

        Task<Showtime> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Showtime>> GetAllAsync(CancellationToken cancellationToken);
    }
}