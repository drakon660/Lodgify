using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IEnumerable<AuditoriumDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _auditoriumService.GetAll(cancellationToken);
    }
}