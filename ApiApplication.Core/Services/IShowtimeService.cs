using ApiApplication.Core.Dtos;

namespace ApiApplication.Core.Services;

public interface IShowtimeService
{
    Task<IEnumerable<ShowtimeDto>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken);
    Task<ReservationDto> BookShowtime(CreateReservationDto createReservationDto, CancellationToken cancellationToken);
}