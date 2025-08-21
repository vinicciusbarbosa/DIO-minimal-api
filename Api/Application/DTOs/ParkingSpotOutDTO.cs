using minimal_api.Api.Domain.Enums;

public record ParkingSpotOutDTO
{
    public int Id { get; set; }
    public string SpotNumber { get; set; } = default!;
    public ContractType ContractType { get; set; }
    public bool IsOccupied { get; set; }
    public VehicleOutDTO? CurrentVehicle { get; set; }
}