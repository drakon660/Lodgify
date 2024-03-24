using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface IAuditoriumsRepository
    {
        Task<Auditorium> GetAsync(int auditoriumId, CancellationToken cancel);
    }
}