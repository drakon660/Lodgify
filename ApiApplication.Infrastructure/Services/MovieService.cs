using ApiApplication.Core.Dtos;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using Ardalis.Result;
using AutoMapper;

namespace ApiApplication.Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMapper mapper, IMovieRepository movieRepository)
    {
        _mapper = mapper;
        _movieRepository = movieRepository;
    }
    public async Task<Result<IEnumerable<MovieDto>>> GetAll(CancellationToken cancellationToken)
    {
        var movies =  await _movieRepository.GetAll(cancellationToken);

        if (!movies.Any())
            return Result.NotFound();

        return Result.Success(_mapper.Map<IEnumerable<MovieDto>>(movies));
    }
}