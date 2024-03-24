using ApiApplication.Core.Dtos;
using CSharpFunctionalExtensions;

namespace ApiApplication.Core.Services;

public interface IShowtimeService
{
    Task<IEnumerable<ShowtimeDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ShowtimeDto>> CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken);
    Task<ReservationDto> ReserveShowtime(CreateReservationDto createReservationDto, CancellationToken cancellationToken);
}