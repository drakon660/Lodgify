using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
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
    public async Task<IEnumerable<ShowtimeDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _showtimeService.GetAllAsync(cancellationToken);
    }

    [HttpPost("create-showtime")]
    public async Task<IActionResult> CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken)
    {
        await _showtimeService.CreateShowtime(createShowtimeDto, cancellationToken);
        return Ok();
    }

    [HttpPost("reserve-showtime")]
    public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto, CancellationToken cancellationToken)
    {
        await _showtimeService.ReserveShowtime(createReservationDto, cancellationToken);
        return Ok();
    }
}