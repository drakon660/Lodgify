using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAll(CancellationToken cancellationToken);
    Task<Movie> GetByName(string title, CancellationToken cancellationToken);
    ValueTask<Movie> GetById(int id, CancellationToken cancellationToken);
}