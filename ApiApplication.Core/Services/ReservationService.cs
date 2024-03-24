using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace ApiApplication.Core.Services;

public interface IReservationService
{
    Task<Reservation> Create();


    // public async Task<ReservationDto> CreateReservation(string movieTitle, IEnumerable<SeatDto> seatsToReserve, DateTime reservationDate, CancellationToken token)
    // {
    //     var showtime = await _showtimesRepository.GetByMovieTitleAsync(movieTitle);
    //
    //      var seats = _mapper.Map<IEnumerable<SeatDto>,ICollection<Seat>>(seatsToReserve);
    //      
    //      var (allFree, cccupied) = showtime.AreSeatsFree(seats);
    //
    //      if (allFree)
    //      {
    //          var reservation = Reservation.Create(showtime, seats, reservationDate);
    //
    //          if (reservation.IsSuccess)
    //          {
    //              _showtimesRepository.UpdateShowTime(showtime);
    //              
    //              return _mapper.Map<Reservation, ReservationDto>(reservation.Value); 
    //          }
    //      }
    //
    //      return null;
    // }

    // public async Task<Result<TicketDto>> ConfirmReservation(Guid reservationId)
    // {
    //      var reservation = await _reservationRepository.GetByIdAsync(reservationId);
    //
    //      var ticketResult = Ticket.Create(reservation, () => DateTime.Now);
    //
    //      if (ticketResult.IsSuccess)
    //      {
    //          return Result.Success(new TicketDto
    //          {
    //              TicketId = ticketResult.Value.Id,
    //              MovieTitle = ticketResult.Value.Showtime.Movie.Title,
    //              AuditoriumName = ticketResult.Value.Showtime.Auditorium.Name,
    //              Seats = ticketResult.Value.Seats.Select(x=>new SeatDto
    //              {
    //                  RowNumber = x.Position.RowNumber,
    //                  SeatNumber = x.Position.SeatNumber
    //              })
    //          });
    //      }
    //
    //      return Result.Failure<TicketDto>("Failed to confirmed");
    // }
}