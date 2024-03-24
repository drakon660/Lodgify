using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface IAuditoriumsRepository
    {
        Task<Auditorium> GetAsync(int auditoriumId, CancellationToken cancel);
        Task<Auditorium> GetById(int auditoriumId, CancellationToken cancellationToken);
    }
}