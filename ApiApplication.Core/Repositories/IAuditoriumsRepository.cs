using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface IAuditoriumsRepository
    {
        Task<IEnumerable<Auditorium>> GetAll(CancellationToken cancellationToken);
        Task<Auditorium> GetById(int auditoriumId, CancellationToken cancellationToken);
    }
}