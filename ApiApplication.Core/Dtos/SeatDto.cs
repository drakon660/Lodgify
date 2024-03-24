namespace ApiApplication.Core.Dtos;

public record SeatDto
{
    public ushort RowNumber { get; init; }
    public ushort SeatNumber { get; init; }
}