using ApiApplication.Core.Dtos;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using AutoMapper;

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
    public async Task<IEnumerable<AuditoriumDto>> GetAll(CancellationToken cancellationToken)
    {
        var auditoriums = await _auditoriumsRepository.GetAll(cancellationToken);
        return _mapper.Map<IEnumerable<AuditoriumDto>>(auditoriums);
    }
}