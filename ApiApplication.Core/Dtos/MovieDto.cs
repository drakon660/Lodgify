namespace ApiApplication.Core.Dtos;

public record MovieDto
{
    public string Name { get; init; }
    public int MovieId { get; init; }
}