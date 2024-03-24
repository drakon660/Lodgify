using ApiApplication.Core.Dtos;

namespace ApiApplication.Core.Services;

public interface IMovieService
{
    public Task<IEnumerable<MovieDto>> GetAll(CancellationToken cancellationToken);
    public Task<MovieDto> GetMovieByTitle(string title, CancellationToken cancellationToken);
}