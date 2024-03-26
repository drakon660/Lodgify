using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Services;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Rest = ApiApplication.Core.Dtos;

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
    public async Task<Result<IEnumerable<Rest.ShowtimeDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllShowtimes(cancellationToken);
    }
    
    [HttpGet("reservations")]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<Rest.ReservationDto>>> GetAllReservations(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllReservations(cancellationToken);
    }
    
    [HttpGet("tickets")]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<Rest.TicketDto>>> GetAllTickets(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllTickets(cancellationToken);
    }
    
    [HttpPost("create-showtime")]
    [TranslateResultToActionResult]
    public async Task<Result<Rest.CreatedShowtimeDto>> CreateShowtime(Rest.CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken)
    {
        return await _showtimeService.CreateShowtime(createShowtimeDto, cancellationToken);
    }
    
    [HttpPost("reserve-showtime")]
    [TranslateResultToActionResult]
    public async Task<Result<Rest.ReservationDto>> CreateReservation(Rest.CreateReservationDto createReservationDto, CancellationToken cancellationToken)
    {
        return await _showtimeService.ReserveShowtime(createReservationDto, cancellationToken);
    }
    
    [HttpPost("confirm-ticket")]
    [TranslateResultToActionResult]
    public async Task<Result<Rest.TicketDto>> ConfirmTicket(Guid reservationId, CancellationToken cancellationToken)
    {
        return await _showtimeService.ConfirmReservation(reservationId, cancellationToken);
    }
}