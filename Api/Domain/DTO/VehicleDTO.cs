using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Domain.DTO
{
    public record VehicleDTO
    {
        public string Name { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public int Year { get; set; } = default!;
    }
}