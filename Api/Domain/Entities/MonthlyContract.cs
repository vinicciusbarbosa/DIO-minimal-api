namespace minimal_api.Api.Domain.Entities
{
    public class MonthlyContract : Contract
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal? DiscountPercent { get; set; }
    }
}   