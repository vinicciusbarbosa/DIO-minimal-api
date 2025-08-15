public record UpdateMonthlyContractDTO
{
    public DateTime? EndDate { get; init; }
    public decimal? MonthlyFee { get; init; }
    public decimal? DiscountPercent { get; init; }
    public bool? Active { get; init; }
    public UpdateVehicleDTO? Vehicle { get; init; }
}

public record UpdateVehicleDTO
{
    public string? Plate { get; init; }
    public string? Name { get; init; }
    public string? Brand { get; init; }
    public string? Color { get; init; }
    public int? Year { get; init; }
}
