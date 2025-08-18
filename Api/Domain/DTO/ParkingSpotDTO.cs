using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.DTO
{
    public class ParkingSpotDTO
    {
        public string SpotNumber { get; set; } = default!;
        public ContractType ContractType { get; set; } = default!;
    }
}