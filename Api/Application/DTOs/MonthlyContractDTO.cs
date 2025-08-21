public record MonthlyContractDTO
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; } 
    public decimal MonthlyFee { get; init; }
    public decimal? DiscountPercent { get; init; }
    public string VehiclePlate { get; init; } = default!;
    public string VehicleName { get; init; } = default!;
    public string VehicleBrand { get; init; } = default!;
    public string VehicleColor { get; init; } = default!;
    public int VehicleYear { get; init; }
}
