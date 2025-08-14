using minimal_api.Api.Domain.Enums;

public record ContractSummaryDTO
{
    public int Id { get; init; }
    public ContractType ContractType { get; init; }
    public DateTime EntryTime { get; init; }
    public DateTime? ExitTime { get; init; }
    public decimal? TotalCost { get; init; }
    public int? AdditionalHours { get; init; }
    public decimal? MonthlyFee { get; init; }
    public decimal? DiscountPercent { get; init; }
    public bool? Active { get; init; }
}