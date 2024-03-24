namespace ApiApplication.Core.Dtos;

public record ReservationDto
{
    public Guid ReservationId { get; init; }
    
    public string MovieTitle { get; init; }
    
    public string AuditoriumName { get; init; }

    public IEnumerable<SeatDto> Seats { get; init; }
}