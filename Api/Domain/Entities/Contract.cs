using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using minimal_api.Domain.Entities;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Api.Domain.Entities
{
    public abstract class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = default!;
        public int ParkingSpotId { get; set; }
        [ForeignKey("ParkingSpotId")]
        public ParkingSpot Spot { get; set; } = default!;
        [Required]
        public ContractType ContractType { get; set; }
        public DateTime EntryTime { get; set; } = DateTime.Now;
        public DateTime? ExitTime { get; set; }
        public bool Active { get; set; } = true;
    }
}