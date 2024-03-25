using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers;

[ApiController]
[Route("showtimes")]
public class ShowTimeController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;

    public ShowTimeController(IShowtimeService showtimeService)
    {
        _showtimeService = showtimeService;
    }

    [HttpGet]
    [TranslateResultToActionResult]
    [ProducesResponseType(statusCode:StatusCodes.Status200OK, type: typeof(IEnumerable<ShowtimeDto>))]
    [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]
  
    public async Task<Result<IEnumerable<ShowtimeDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllShowtimes(cancellationToken);
    }
    
    [HttpGet("reservations")]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<ReservationDto>>> GetAllReservations(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllReservations(cancellationToken);
    }
    
    [HttpGet("tickets")]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<TicketDto>>> GetAllTickets(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllTickets(cancellationToken);
    }

    [HttpPost("create-showtime")]
    [TranslateResultToActionResult]
    public async Task<Result<CreatedShowtimeDto>> CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken)
    {
        return await _showtimeService.CreateShowtime(createShowtimeDto, cancellationToken);
    }

    [HttpPost("reserve-showtime")]
    [TranslateResultToActionResult]
    public async Task<Result<ReservationDto>> CreateReservation(CreateReservationDto createReservationDto, CancellationToken cancellationToken)
    {
        return await _showtimeService.ReserveShowtime(createReservationDto, cancellationToken);
    }
    
    [HttpPost("confirm-ticket")]
    [TranslateResultToActionResult]
    public async Task<Result<TicketDto>> ConfirmTicket(Guid reservationId, CancellationToken cancellationToken)
    {
        return await _showtimeService.ConfirmReservation(reservationId, cancellationToken);
    }
}