using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Api.Domain.Entities;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.Entities
{
public class Vehicle
{
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(10)]
    public string Plate { get; set; } = default!;
    public int? ParkingSpotId { get; set; }
    [ForeignKey("ParkingSpotId")]
    public ParkingSpot? ParkingSpot { get; set; }

    [StringLength(150)]
    public string? Name { get; set; } 

    [StringLength(100)]
    public string? Brand { get; set; } 

    [StringLength(50)]
    public string? Color { get; set; } 
    
    public int Year { get; set; }
}

}