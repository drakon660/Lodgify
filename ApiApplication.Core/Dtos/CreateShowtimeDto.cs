namespace ApiApplication.Core.Dtos;

public class CreateShowtimeDto
{
    public int MovieId { get; init; }
    public int AuditoriumId { get; init; }
    public DateTime SessionDate { get; init; }
}