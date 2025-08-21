using minimal_api.Domain.Enums;

namespace minimal_api.Domain.DTO
{
    public record AdministratorDTO
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Profile Profile { get; set; } = default!;
    }
}