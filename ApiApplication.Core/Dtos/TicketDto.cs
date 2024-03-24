namespace ApiApplication.Core.Dtos;

public record TicketDto
{
    public Guid TicketId { get; init; }
    
    public string MovieTitle { get; init; }
    
    public string AuditoriumName { get; init; }

    public IEnumerable<SeatDto> Seats { get; init; }
}