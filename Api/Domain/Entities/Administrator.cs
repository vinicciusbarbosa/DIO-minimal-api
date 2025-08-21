using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using minimal_api.Domain.Enums;

namespace minimal_api.Domain.Entities
{
    public class Administrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = default!;
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = default!;
        [Required]
        [StringLength(10)]
        public Profile Profile { get; set; } = default!;
    }
}