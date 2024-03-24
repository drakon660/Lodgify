using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Services;

public interface IAuditoriumService
{
    public Task<IEnumerable<AuditoriumDto>> GetAll(CancellationToken cancellationToken);
}