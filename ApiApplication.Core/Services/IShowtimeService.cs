using ApiApplication.Core.Dtos;
using Ardalis.Result;
namespace ApiApplication.Core.Services;

public interface IShowtimeService
{
    Task<Result<IEnumerable<ShowtimeDto>>> GetAllShowtimes(CancellationToken cancellationToken);
    Task<Result<IEnumerable<ReservationDto>>> GetAllReservations(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TicketDto>>> GetAllTickets(CancellationToken cancellationToken);
    Task<Result<CreatedShowtimeDto>> CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken);

    Task<Result<ReservationDto>> ReserveShowtime(CreateReservationDto createReservationDto,
        CancellationToken cancellationToken);

    Task<Result<TicketDto>> ConfirmReservation(Guid reservationId, CancellationToken cancellationToken);
}