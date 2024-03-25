namespace ApiApplication.Core.Dtos;

public record ShowtimeDto
{
    public string Title { get; init; }
    public string AuditoriumName { get; init; }
    
    public DateTime SessionDate { get; init; }
    
    public IEnumerable<SeatDto> FreeSeats { get; init; }
}