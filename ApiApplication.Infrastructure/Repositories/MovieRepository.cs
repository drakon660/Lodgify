using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaContext _cinemaContext;
    
    public MovieRepository(CinemaContext cinemaContext)
    {
        _cinemaContext = cinemaContext;
    }
    
    public async Task<IEnumerable<Movie>> GetAll(CancellationToken cancellationToken)
    {
        return await _cinemaContext.Movies.ToListAsync(cancellationToken);
    }

    public async Task<Movie> GetByName(string title, CancellationToken cancellationToken)
    {
        return await _cinemaContext.Movies.Where(x => x.Title == title).FirstOrDefaultAsync(cancellationToken);
    }

    public ValueTask<Movie> GetById(int id, CancellationToken cancellationToken)
    {
        return _cinemaContext.Movies.FindAsync(id, cancellationToken);
    }
}