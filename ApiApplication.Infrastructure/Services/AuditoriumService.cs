using ApiApplication.Core.Dtos;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ApiApplication.Infrastructure.Services;

public class AuditoriumService : IAuditoriumService
{
    private readonly IMapper _mapper;
    private readonly IAuditoriumsRepository _auditoriumsRepository;

    public AuditoriumService(IMapper mapper, IAuditoriumsRepository auditoriumsRepository)
    {
        _mapper = mapper;
        _auditoriumsRepository = auditoriumsRepository;
    }
    public async Task<Result<IEnumerable<AuditoriumDto>>> GetAll(CancellationToken cancellationToken)
    {
        var auditoriums = await _auditoriumsRepository.GetAll(cancellationToken);

        if (!auditoriums.Any())
            return Result.NotFound();

        return Result.Success(_mapper.Map<IEnumerable<AuditoriumDto>>(auditoriums));
    }
}