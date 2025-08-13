using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = default!;
        [Required]
        [StringLength(100)]
        public string Brand { get; set; } = default!;
        [Required]
        public int Year { get; set; } = default!;
    }
}