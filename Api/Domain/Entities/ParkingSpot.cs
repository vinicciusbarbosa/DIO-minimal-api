using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.Entities
{
    public class ParkingSpot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SpotNumber { get; set; } = default!;
        public ContractType ContractType { get; set; } = default!;
        public bool IsOccupied { get; set; } = false;
        [ForeignKey("CurrentVehicleId")]
        public Vehicle? CurrentVehicle { get; set; }
        public int? CurrentVehicleId { get; set; }
    }
}
