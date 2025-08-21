using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.DTO
{
    public record ParkingSpotDTO
    {
        public string SpotNumber { get; set; } = default!;
        public ContractType ContractType { get; set; } = default!;
    }
}