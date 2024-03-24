using ApiApplication.Core.Dtos;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
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
    public async Task<IEnumerable<MovieDto>> GetAll(CancellationToken cancellationToken)
    {
        var movies =  await _movieRepository.GetAll(cancellationToken);
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieDto> GetMovieByTitle(string title, CancellationToken cancellationToken)
    {
        var movie =  await _movieRepository.GetByName(title,cancellationToken);
        return _mapper.Map<MovieDto>(movie);
    }
}