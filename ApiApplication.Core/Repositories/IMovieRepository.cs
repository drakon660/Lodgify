using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories;

public interface IMovieRepository
{
    Movie GetMovieByImdbId(string id);
}