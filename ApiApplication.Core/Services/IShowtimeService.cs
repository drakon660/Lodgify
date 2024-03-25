using ApiApplication.Core.Dtos;
using CSharpFunctionalExtensions;

namespace ApiApplication.Core.Services;

public interface IShowtimeService
{
    Task<IEnumerable<ShowtimeDto>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<ReservationDto>> GetAllReservations(CancellationToken cancellationToken);
    Task<IEnumerable<TicketDto>> GetAllTickets(CancellationToken cancellationToken);
    Task<Result<ShowtimeDto>> CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken);
    Task<ReservationDto> ReserveShowtime(CreateReservationDto createReservationDto, CancellationToken cancellationToken);
    Task<TicketDto> ConfirmReservation(Guid reservationId, CancellationToken cancellationToken);
}