using ApiApplication.Core.Dtos;

namespace ApiApplication.Core.Services;

public interface IShowtimeService
{
    Task<IEnumerable<ShowtimeDto>> GetAllAsync(CancellationToken cancellationToken);

    Task Create(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken);
}