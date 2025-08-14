public record RotativeCheckOutDTO
{
    public string Plate { get; init; } = default!;
    public DateTime ExitTime { get; init; } = DateTime.Now;
}