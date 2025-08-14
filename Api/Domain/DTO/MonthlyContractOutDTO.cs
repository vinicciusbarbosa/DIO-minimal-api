public record MonthlyContractOutDTO
{
    public int Id { get; init; }
    public string Plate { get; init; } = default!;
    public int ParkingSpotId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public decimal MonthlyFee { get; init; }
    public decimal? DiscountPercent { get; init; }
    public bool Active { get; init; }
}
