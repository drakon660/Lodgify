using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Controllers;

[ApiController]
[Route("auditoriums")]
public class AuditoriumController : ControllerBase
{
    private readonly IAuditoriumService _auditoriumService;

    public AuditoriumController(IAuditoriumService auditoriumService)
    {
        _auditoriumService = auditoriumService;
    }

    [HttpGet]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<AuditoriumDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await _auditoriumService.GetAll(cancellationToken);
    }
}