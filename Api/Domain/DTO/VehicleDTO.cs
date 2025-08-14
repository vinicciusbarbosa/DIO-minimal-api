public record VehicleDTO
{
    public string Plate { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Brand { get; init; } = default!;
    public string Color { get; init; } = default!;
    public int Year { get; init; }
}
