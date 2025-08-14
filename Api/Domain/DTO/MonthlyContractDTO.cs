public record MonthlyContractDTO
{
    public string Plate { get; init; } = default!;
    public int ParkingSpotId { get; init; }
    public DateTime StartDate { get; init; }
    public decimal MonthlyFee { get; init; }
    public decimal? DiscountPercent { get; init; }
}
