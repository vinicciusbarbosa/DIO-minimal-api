public record MonthlyContractOutDTO
{
    public int Id { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public decimal MonthlyFee { get; init; }
    public decimal? DiscountPercent { get; init; }
    public bool Active { get; init; }
    public VehicleOutDTO Vehicle { get; init; } = default!;
    public int? ParkingSpotId { get; set; }
}

public record VehicleOutDTO
{
    public int Id { get; init; }
    public string Plate { get; init; } = default!;
    public string? Name { get; init; } = default!;
    public string? Brand { get; init; } = default!;
    public string? Color { get; init; } = default!;
    public int Year { get; init; }
}