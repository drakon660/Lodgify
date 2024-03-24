using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ApiApplication.Controllers;


[ApiController]
[Route("showtimes")]
public class ShowTimeController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;

    public ShowTimeController(IShowtimeService  showtimeService)
    {
        _showtimeService = showtimeService;
    }
    public async Task<IEnumerable<ShowtimeDto>> GetAll() 
    {
         return await _showtimeService.GetAllAsync();
    }
}