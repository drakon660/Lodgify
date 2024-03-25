using ApiApplication.Core.Dtos;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Infrastructure.Services;

public class AuditoriumService : IAuditoriumService
{
    private readonly ILogger<AuditoriumService> _logger;
    private readonly IMapper _mapper;
    private readonly IAuditoriumsRepository _auditoriumsRepository;

    public AuditoriumService(ILogger<AuditoriumService> logger, IMapper mapper, IAuditoriumsRepository auditoriumsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _auditoriumsRepository = auditoriumsRepository;
    }
    public async Task<Result<IEnumerable<AuditoriumDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAll");
        
        var auditoriums = await _auditoriumsRepository.GetAll(cancellationToken);

        if (!auditoriums.Any())
            return Result.NotFound();

        return Result.Success(_mapper.Map<IEnumerable<AuditoriumDto>>(auditoriums));
    }
}