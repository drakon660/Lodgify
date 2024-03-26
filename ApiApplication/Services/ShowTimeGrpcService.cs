using System;
using System.Linq;
using System.Threading.Tasks;
using Rest = ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc;
using Grpc.Core;

namespace ApiApplication.Services;

public class ShowTimeGrpcService : ShowtimeService.ShowtimeServiceBase
{
    private readonly IShowtimeService _showtimeService;
    
    public ShowTimeGrpcService(IShowtimeService showtimeService)
    {
        _showtimeService = showtimeService;
    }
    
    public override async Task<CreatedShowtimeResponse> CreateShowtime(CreateShowtimeRequest request, ServerCallContext context)
    {
        var createdShowtimeResult = await _showtimeService.CreateShowtime(new Rest.CreateShowtimeDto
        {
            AuditoriumId = request.AuditoriumId,
            MovieId = request.MovieId,
            SessionDate = request.SessionDate.ToDateTime()
        }, context.CancellationToken);
    
        if (createdShowtimeResult.IsSuccess)
        {
            var showtime = createdShowtimeResult.Value;
    
            return new CreatedShowtimeResponse()
            {
                SessionDate = Timestamp.FromDateTime(showtime.SessionDate),
                ShowtimeId = showtime.ShowtimeId,
                Title = showtime.Title,
                AuditoriumName = showtime.AuditoriumName,
                Status = new StatusDto()
                {
                    Code = 0,
                    Message = string.Empty
                }
            };
        }

        return new CreatedShowtimeResponse()
        {
            Status = new StatusDto()
            {
                Code = 1,
                Message = string.Join("\r\n", createdShowtimeResult.ValidationErrors.Select(x => x.ErrorMessage))
            }
        };
    }
    public override async Task<GetAllResponse> GetAllShowtimes(GetAllRequest request, ServerCallContext context)
    {
        var showtimesResult = await _showtimeService.GetAllShowtimes(context.CancellationToken);
        
        if (showtimesResult.IsSuccess)
        {
            return new GetAllResponse
            {
                Showtimes =
                {
                    showtimesResult.Value.Select(x => new ShowtimeDto
                    {
                        Title = x.Title,
                        AuditoriumName = x.AuditoriumName,
                        SessionDate = Timestamp.FromDateTime(x.SessionDate),
                        ShowtimeId = x.ShowtimeId,
                        FreeSeats = { x.FreeSeats.Select(y=>new SeatDto
                        {
                            RowNumber = y.RowNumber,
                            SeatNumber = y.SeatNumber
                        }) }
                    })
                }
            };
        }

        return new GetAllResponse
        {
            Status = new StatusDto()
            {
                Code = 1,
                Message = showtimesResult.Status.ToString()
            }
        };
    }

    public override async Task<CreateReservationResponse> CreateReservation(CreateReservationRequest request, ServerCallContext context)
    {
        var reservationResult = await _showtimeService.ReserveShowtime(new Rest.CreateReservationDto
        {
            ShowTimeId = request.ShowTimeId,
            Seats = request.Seats.Select(x=>new Core.Dtos.SeatDto
            {
                RowNumber = (ushort)x.RowNumber,
                SeatNumber = (ushort)x.SeatNumber,
            })
        }, context.CancellationToken);
    
        if (reservationResult.IsSuccess)
        {
            var reservation = reservationResult.Value;

            return new CreateReservationResponse
            {
                Reservation = new ReservationDto
                {
                    ReservationId = reservation.ReservationId.ToString(),
                    AuditoriumName = reservation.AuditoriumName,
                    IsConfirmed = reservation.IsConfirmed,
                    IsExpired = reservation.IsExpired,
                    MovieTitle = reservation.MovieTitle,
                    Seats =
                    {
                        reservation.Seats.Select(x => new SeatDto
                        {
                            RowNumber = x.RowNumber,
                            SeatNumber = x.SeatNumber
                        })
                    }
                }
            };
        }

        return new CreateReservationResponse
        {
            Status = new StatusDto()
            {
                Code = 1,
                Message = string.Join("\r\n", reservationResult.ValidationErrors.Select(x => x.ErrorMessage))
            }
        };
    }
    public override async Task<ConfirmTicketResponse> ConfirmTicket(ConfirmTicketRequest request, ServerCallContext context)
    {
        var ticketResult = await 
            _showtimeService.ConfirmReservation(Guid.Parse(request.ReservationId), context.CancellationToken);
    
        if (ticketResult.IsSuccess)
        {
            var ticket = ticketResult.Value;
            return new ConfirmTicketResponse
            {
                Ticket = new TicketDto
                {
                    AuditoriumName = ticket.AuditoriumName,
                    MovieTitle = ticket.MovieTitle,
                    TicketId = ticket.TicketId.ToString(),
                    Seats =
                    {
                        ticket.Seats.Select(x => new SeatDto
                        {
                            RowNumber = x.RowNumber,
                            SeatNumber = x.SeatNumber
                        })
                    }
                }
            };
        }

        return new ConfirmTicketResponse
        {
            Status = new StatusDto()
            {
                Code = 1,
                Message = string.Join("\r\n", ticketResult.ValidationErrors.Select(x => x.ErrorMessage))
            }
        };
    }

    public override async Task<GetAllTicketsResponse> GetAllTickets(GetAllTicketsRequest request, ServerCallContext context)
    {
        var ticketsResult = await _showtimeService.GetAllTickets(context.CancellationToken);
    
        if (ticketsResult.IsSuccess)
        {
            var tickets = ticketsResult.Value;
            return new GetAllTicketsResponse
            {
                Tickets =
                {
                    tickets.Select(x => new TicketDto
                    {
                        AuditoriumName = x.AuditoriumName,
                        TicketId = x.TicketId.ToString(),
                        MovieTitle = x.MovieTitle,
                        Seats =
                        {
                            x.Seats.Select(y => new SeatDto
                            {
                                RowNumber = y.RowNumber,
                                SeatNumber = y.SeatNumber
                            })
                        }
                    })
                }
            };
        }

        return new GetAllTicketsResponse
        {
            Status = new StatusDto
            {
                Code = 1,
                Message = ticketsResult.Status.ToString()
            }
        };
    }

    public override async Task<GetAllReservationsResponse> GetAllReservations(GetAllReservationsRequest request, ServerCallContext context)
    {
        var reservationsResult = await _showtimeService.GetAllReservations(context.CancellationToken);

        if (reservationsResult.IsSuccess)
        {
            var reservations = reservationsResult.Value;

            return new GetAllReservationsResponse
            {
                Reservations =
                {
                    reservations.Select(x => new ReservationDto
                    {
                        ReservationId = x.ReservationId.ToString(),
                        AuditoriumName = x.AuditoriumName,
                        MovieTitle = x.MovieTitle,
                        IsConfirmed = x.IsConfirmed,
                        IsExpired = x.IsExpired,
                        Seats = {  x.Seats.Select(y => new SeatDto
                        {
                            RowNumber = y.RowNumber,
                            SeatNumber = y.SeatNumber
                        }) }
                    })
                }
            };
        }
        
        return new GetAllReservationsResponse
        {
            Status = new StatusDto
            {
                Code = 1,
                Message = reservationsResult.Status.ToString()
            }
        };
    }
}