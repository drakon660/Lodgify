using ApiApplication.Core.Dtos;
using Ardalis.Result;

namespace ApiApplication.Core.Services;

public interface IMovieService
{
    Task<Result<IEnumerable<MovieDto>>> GetAll(CancellationToken cancellationToken);
}