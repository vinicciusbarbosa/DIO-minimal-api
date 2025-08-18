using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.DTO
{
    public record UpdateParkingSpotDTO
    {
        public string? SpotNumber { get; init; }
        public ContractType? ContractType { get; init; }
        public bool? IsOccupied { get; init; }
        public int? CurrentVehicleId { get; set; }
    }
}