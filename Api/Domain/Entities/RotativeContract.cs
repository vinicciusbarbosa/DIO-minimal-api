using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Api.Domain.Entities
{
    public class RotativeContract : Contract
    {
        public decimal PricePerHour { get; set; } = 10m;
        public decimal? TotalCost { get; set; } = default!;
        public int? AdditionalHours { get; set; }
        public int TotalHours { get; set; }
    }
}