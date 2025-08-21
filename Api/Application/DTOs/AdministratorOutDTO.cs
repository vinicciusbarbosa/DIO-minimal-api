using minimal_api.Domain.Enums;

namespace minimal_api.Domain.DTO
{
    public record AdministratorOutDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public Profile Profile { get; set; } = default!;
    }
}
