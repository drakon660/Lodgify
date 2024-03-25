using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using Ardalis.Result;

namespace ApiApplication.Core.Services;

public interface IAuditoriumService
{
    Task<Result<IEnumerable<AuditoriumDto>>> GetAll(CancellationToken cancellationToken);
}