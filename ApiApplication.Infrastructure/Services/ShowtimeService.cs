using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using AutoMapper;

namespace ApiApplication.Infrastructure.Services;

public class ShowtimeService : IShowtimeService
{
    private readonly IMapper _mapper;
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IAuditoriumsRepository _auditoriumsRepository;
    public ShowtimeService(IMapper mapper, IShowtimeRepository showtimeRepository, IMovieRepository movieRepository, IAuditoriumsRepository auditoriumsRepository)
    {
        _mapper = mapper;
        _showtimeRepository = showtimeRepository;
        _movieRepository = movieRepository;
        _auditoriumsRepository = auditoriumsRepository;
    }
    public async Task<IEnumerable<ShowtimeDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var showtimes = await _showtimeRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<Showtime>, IEnumerable<ShowtimeDto>>(showtimes);
    }

    public async Task Create(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetById(createShowtimeDto.MovieId, cancellationToken);
        var auditorium = await _auditoriumsRepository.GetById(createShowtimeDto.AuditoriumId, cancellationToken);
        
        Showtime showtime = Showtime.Create(movie, createShowtimeDto.SessionDate, auditorium);

        await _showtimeRepository.CreateShowtime(showtime, cancellationToken);
    }
}