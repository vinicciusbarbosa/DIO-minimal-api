public record RotativeCheckInDTO
{
    public string Plate { get; init; } = default!;
    public int ParkingSpotId { get; init; }
}
